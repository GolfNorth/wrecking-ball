using Game.Presentation.Views;

namespace Game.Presentation.Events
{
    /// <summary>
    /// Событие освобождения шара
    /// </summary>
    public class BallReleasedEvent
    {
        public bool GotPoints { get; }
        
        public BallView BallView { get; }

        public BallReleasedEvent(BallView ballView, bool gotPoints)
        {
            BallView = ballView;
            GotPoints = gotPoints;
        }
    }
}