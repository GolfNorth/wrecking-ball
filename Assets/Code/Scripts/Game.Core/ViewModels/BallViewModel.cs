using UniRx;
using UnityEngine;

namespace Game.ViewModels
{
    /// <summary>
    /// Вью-модель шара
    /// </summary>
    public class BallViewModel
    {
        public ReadOnlyReactiveProperty<Color> Color { get; }

        public ReadOnlyReactiveProperty<int> Points { get; }

        public BallViewModel(Color color, int points)
        {
            Color = new ReactiveProperty<Color>(color).ToReadOnlyReactiveProperty();
            Points = new ReactiveProperty<int>(points).ToReadOnlyReactiveProperty();
        }
    }
}