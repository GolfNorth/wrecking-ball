namespace Game.Events
{
    /// <summary>
    /// Событие изменеия счета
    /// </summary>
    public class GameScoreChangedEvent
    {
        public int Score { get; }

        public GameScoreChangedEvent(int score)
        {
            Score = score;
        }
    }
}