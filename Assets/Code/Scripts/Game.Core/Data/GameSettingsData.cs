using System;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Create Game Settings Data", fileName = "GameSettingsData", order = 0)]
    public class GameSettingsData : ScriptableObject
    {
        [SerializeField]
        private int towerWidth;

        [SerializeField]
        private int towerHeight;

        [SerializeField]
        private int matchLength;

        [SerializeField]
        private ColorSettings[] colorSettings = Array.Empty<ColorSettings>();

        public GameSettings CreateSettings()
        {
            return new GameSettings(colorSettings, towerWidth, towerHeight, matchLength);
        }
    }
}