using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class ColorSettings
    {
        [field: SerializeField, Tooltip("Цвет шара")]
        public Color Color { get; private set; }

        [field: SerializeField, Tooltip("Количество очков за уничтожение шара")]
        public int Points { get; private set; }
    }
}