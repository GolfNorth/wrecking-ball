using Game.ViewModels;

namespace Game.Events
{
    public class BallDestroyedEvent
    {
        public BallViewModel BallViewModel { get; }

        public BallDestroyedEvent(BallViewModel ballViewModel)
        {
            BallViewModel = ballViewModel;
        }
    }
}