using Game.ViewModels;

namespace Game.Events
{
    /// <summary>
    /// Событие падения шара за пределы
    /// </summary>
    public class BallFallenEvent
    {
        public BallViewModel BallViewModel { get; }

        public BallFallenEvent(BallViewModel ballViewModel)
        {
            BallViewModel = ballViewModel;
        }
    }
}