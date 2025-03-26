using System;
using Game.ViewModels;
using UniRx;
using UnityEngine;

namespace Game.Presentation.Views
{
    /// <summary>
    /// Визуализатор шара
    /// </summary>
    public class BallView : BaseView<BallViewModel>
    {
        [SerializeField]
        private Rigidbody2D rigidbody2D;

        public ReactiveProperty<bool> IsKinematic { get; } = new();

        private void OnEnable()
        {
            IsKinematic.Subscribe(b => rigidbody2D.isKinematic = b).AddTo(Disposables);
        }
    }
}