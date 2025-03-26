using Game.Presentation.Views;
using Game.ViewModels;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Presentation.Components
{
    /// <summary>
    /// Компонент смены цвета
    /// </summary>
    public class BallColorizer : BaseViewComponent<BallView, BallViewModel>
    {
        [FormerlySerializedAs("graphic")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        protected override void OnDataContextChanged()
        {
            spriteRenderer.color = DataContext.Color.Value;
        }
    }
}