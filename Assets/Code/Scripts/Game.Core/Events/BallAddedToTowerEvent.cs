using Game.ViewModels;
using UnityEngine;

namespace Game.Events
{
    /// <summary>
    /// Событие добавления шара в башню
    /// </summary>
    public class BallAddedToTowerEvent
    {
        public BallViewModel BallViewModel { get; }

        public Vector2Int Position { get; }

        public BallAddedToTowerEvent(BallViewModel ballViewModel, Vector2Int position)
        {
            BallViewModel = ballViewModel;
            Position = position;
        }
    }
}