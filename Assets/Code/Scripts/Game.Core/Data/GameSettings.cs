using System.Collections.Generic;
using System.Linq;

namespace Game.Data
{
    /// <summary>
    /// Игровые параметры
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Ширина башни
        /// </summary>
        public int TowerWidth { get; }

        /// <summary>
        /// Высота башни
        /// </summary>
        public int TowerHeight { get; }

        /// <summary>
        /// Длина ряда шаров
        /// </summary>
        public int MatchLength { get; }

        /// <summary>
        /// Параметры цветов шаров
        /// </summary>
        public IReadOnlyList<ColorSettings> ColorSettings { get; }

        public GameSettings(IEnumerable<ColorSettings> colorSettings, int towerWidth, int towerHeight, int matchLength)
        {
            TowerWidth = towerWidth;
            TowerHeight = towerHeight;
            MatchLength = matchLength;
            ColorSettings = colorSettings.ToList();
        }
    }
}