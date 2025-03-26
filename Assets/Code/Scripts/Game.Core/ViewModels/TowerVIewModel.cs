using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Game.ViewModels
{
    /// <summary>
    /// Вью-модель башни
    /// </summary>
    public class TowerVIewModel
    {
        private readonly Dictionary<BallViewModel, Vector2Int> _ballToPosition = new();

        private readonly ReactiveDictionary<Vector2Int, BallViewModel> _balls = new();

        /// <summary>
        /// Текущие шары в башне
        /// </summary>
        public IReadOnlyReactiveDictionary<Vector2Int, BallViewModel> Balls => _balls;

        /// <summary>
        /// Следующий появляющийся шар
        /// </summary>
        public ReactiveProperty<BallViewModel> Next { get; } = new();

        /// <summary>
        /// Добавить шар в башню
        /// </summary>
        public void AddBall(Vector2Int position, BallViewModel ball)
        {
            if (_ballToPosition.TryGetValue(ball, out var prevPosition))
            {
                _balls.Remove(prevPosition);
                _ballToPosition.Remove(ball);
            }

            _balls.Add(position, ball);
            _ballToPosition.Add(ball, position);
        }

        /// <summary>
        /// Удалить шар из башни
        /// </summary>
        public void RemoveBall(BallViewModel ball)
        {
            if (!_ballToPosition.TryGetValue(ball, out var position))
                return;

            _balls.Remove(position);
            _ballToPosition.Remove(ball);
        }
    }
}