using Game.ViewModels;

namespace Game.Events
{
    /// <summary>
    /// Событие сброса шара
    /// </summary>
    public class BallDroppedEvent
    {
        public BallViewModel BallViewModel { get; }

        public BallDroppedEvent(BallViewModel ballViewModel)
        {
            BallViewModel = ballViewModel;
        }
    }
}