using System;
using System.Collections.Generic;
using Game.Data;
using Game.ViewModels;
using UnityEngine;

namespace Game.Infrastructure
{
    /// <summary>
    /// Реализация фабрики шаров
    /// </summary>
    public class BallFactory : IBallFactory
    {
        private readonly Dictionary<Color, int> _colorPoints;

        public BallFactory(GameSettings gameSettings)
        {
            _colorPoints = new Dictionary<Color, int>(gameSettings.ColorSettings.Count);

            foreach (var cs in gameSettings.ColorSettings)
            {
                _colorPoints.Add(cs.Color, cs.Points);
            }
        }

        public BallViewModel Get(Color color)
        {
            if (!_colorPoints.TryGetValue(color, out var points))
                throw new ArgumentException();

            return new BallViewModel(color, points);
        }
    }
}