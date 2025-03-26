namespace Game.Infrastructure
{
    /// <summary>
    /// Система наивысшего результата
    /// </summary>
    public interface IHighScoreSystem
    {
        int HighScore { get; }
        int LastScore { get; }
        void Set(int score);
    }
}