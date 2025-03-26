using System;
using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Events;
using Game.Infrastructure;
using Game.SDK.Infrastructure.Interfaces;
using Game.ViewModels;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Game.Context
{
    /// <summary>
    /// Игровой менеджер
    /// </summary>
    public class GameManager : IPostStartable, IDisposable
    {
        private readonly GameSettings _gameSettings;
        private readonly IEventBus _eventBus;
        private readonly IBallFactory _ballFactory;
        private readonly TowerVIewModel _towerVIewModel;
        private readonly CompositeDisposable _disposables = new();
        private readonly IHighScoreSystem _highScoreSystem;
        private int _gameScore;
        private bool _isGameOver;

        public GameManager(GameSettings gameSettings, IEventBus eventBus, IBallFactory ballFactory,
            ITowerFactory towerFactory, IHighScoreSystem highScoreSystem)
        {
            _gameSettings = gameSettings;
            _eventBus = eventBus;
            _ballFactory = ballFactory;
            _towerVIewModel = towerFactory.Get();
            _highScoreSystem = highScoreSystem;
        }

        public void PostStart()
        {
            _eventBus.Subscribe<BallAddedToTowerEvent>(OnBallAddedToTower).AddTo(_disposables);
            _eventBus.Subscribe<BallFallenEvent>(OnBallFallen).AddTo(_disposables);
            _eventBus.Subscribe<BallDroppedEvent>(OnBallDropped).AddTo(_disposables);
            _eventBus.Subscribe<BallOverFilledEvent>(OnBallOverFilled).AddTo(_disposables);

            SetNextBall();
        }

        /// <summary>
        /// Создать следующий шар
        /// </summary>
        private void SetNextBall()
        {
            if (_towerVIewModel.Next.Value != null || _isGameOver)
                return;

            var index = Random.Range(0, _gameSettings.ColorSettings.Count);
            var color = _gameSettings.ColorSettings[index].Color;
            var ball = _ballFactory.Get(color);

            _towerVIewModel.Next.Value = ball;

            _eventBus.Publish(new BallCreatedEvent(ball));
        }

        private void GameOver()
        {
            _isGameOver = true;
            _highScoreSystem.Set(_gameScore);
            _eventBus.Publish(new GameOverEvent());
        }

        #region Handlers

        /// <summary>
        /// Башня переполнена
        /// </summary>
        private void OnBallOverFilled(BallOverFilledEvent e)
        {
            GameOver();
        }

        /// <summary>
        /// Обработка события сброса шара
        /// </summary>
        private void OnBallDropped(BallDroppedEvent e)
        {
            if (_towerVIewModel.Next.Value != e.BallViewModel)
                return;

            _towerVIewModel.Next.Value = null;
        }

        /// <summary>
        /// Обработка добавления шара в башню
        /// </summary>
        private void OnBallAddedToTower(BallAddedToTowerEvent e)
        {
            _towerVIewModel.AddBall(e.Position, e.BallViewModel);

            CheckMatches();
            SetNextBall();
        }

        /// <summary>
        /// Обработка падения шара за пределы
        /// </summary>
        private void OnBallFallen(BallFallenEvent e)
        {
            SetNextBall();

            _gameScore -= e.BallViewModel.Points.Value;

            _eventBus.Publish(new GameScoreChangedEvent(_gameScore));
        }

        #endregion

        #region Matches

        private void CheckMatches()
        {
            var toDestroy = new List<BallViewModel>();

            // Проверка горизонталей
            for (var y = 0; y < _gameSettings.TowerHeight; y++)
            {
                for (var x = 0; x <= _gameSettings.TowerWidth - _gameSettings.MatchLength; x++)
                {
                    if (CheckLine(x, y, 1, 0, _gameSettings.MatchLength, out var matches))
                        toDestroy.AddRange(matches);
                }
            }

            // Проверка вертикалей
            for (var x = 0; x < _gameSettings.TowerWidth; x++)
            {
                for (var y = 0; y <= _gameSettings.TowerHeight - _gameSettings.MatchLength; y++)
                {
                    if (CheckLine(x, y, 0, 1, _gameSettings.MatchLength, out var matches))
                        toDestroy.AddRange(matches);
                }
            }

            // Проверка диагоналей (слева-направо)
            for (var x = 0; x <= _gameSettings.TowerWidth - _gameSettings.MatchLength; x++)
            {
                for (var y = 0; y <= _gameSettings.TowerHeight - _gameSettings.MatchLength; y++)
                {
                    if (CheckLine(x, y, 1, 1, _gameSettings.MatchLength, out var matches))
                        toDestroy.AddRange(matches);
                }
            }

            // Проверка диагоналей (справа-налево)
            for (var x = _gameSettings.MatchLength - 1; x < _gameSettings.TowerWidth; x++)
            {
                for (var y = 0; y <= _gameSettings.TowerHeight - _gameSettings.MatchLength; y++)
                {
                    if (CheckLine(x, y, -1, 1, _gameSettings.MatchLength, out var matches))
                        toDestroy.AddRange(matches);
                }
            }

            if (toDestroy.Count > 0)
                DestroyMatches(toDestroy.Distinct().ToList());

            if (_towerVIewModel.Balls.Count() == _gameSettings.TowerHeight * _gameSettings.TowerWidth)
                GameOver();
        }

        private bool CheckLine(int startX, int startY, int dx, int dy, int length, out List<BallViewModel> matches)
        {
            matches = new List<BallViewModel>();

            for (int i = 0; i < length; i++)
            {
                var x = startX + i * dx;
                var y = startY + i * dy;

                if (!_towerVIewModel.Balls.TryGetValue(new Vector2Int(x, y), out var ball))
                    return false;

                matches.Add(ball);
            }

            var color = matches[0].Color.Value;

            return matches.All(m => m.Color.Value == color);
        }

        private void DestroyMatches(List<BallViewModel> balls)
        {
            foreach (var ball in balls)
            {
                _towerVIewModel.RemoveBall(ball);
                _gameScore += ball.Points.Value;

                _eventBus.Publish(new BallDestroyedEvent(ball));
            }

            _eventBus.Publish(new GameScoreChangedEvent(_gameScore));
        }

        #endregion

        #region Dispose

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposables?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}