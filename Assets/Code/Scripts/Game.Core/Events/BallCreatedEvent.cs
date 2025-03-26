using Game.ViewModels;

namespace Game.Events
{
    /// <summary>
    /// Событие создания шара
    /// </summary>
    public class BallCreatedEvent
    {
        public BallViewModel BallViewModel { get; }

        public BallCreatedEvent(BallViewModel ballViewModel)
        {
            BallViewModel = ballViewModel;
        }
    }
}