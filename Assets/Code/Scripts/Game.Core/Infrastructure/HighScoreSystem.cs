using UnityEngine;

namespace Game.Infrastructure
{
    /// <summary>
    /// Реализация системы лучшего результата
    /// </summary>
    public class HighScoreSystem : IHighScoreSystem
    {
        public int HighScore { get; private set; }

        public int LastScore { get; private set; }

        public HighScoreSystem()
        {
            HighScore = PlayerPrefs.GetInt("hs", 0);
        }

        public void Set(int score)
        {
            LastScore = score;

            if (HighScore > LastScore)
                return;

            HighScore = LastScore;

            PlayerPrefs.SetInt("hs", HighScore);
            PlayerPrefs.Save();
        }
    }
}