using System;
using Game.Infrastructure;
using Game.ViewModels;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Presentation.Views
{
    /// <summary>
    /// Визуализатор башни
    /// </summary>
    public class TowerView : BaseView<TowerVIewModel>
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private string isHiddenTag;

        private int _isHiddenHash;

        [Inject]
        public void Construct(ITowerFactory towerFactory)
        {
            Init(towerFactory.Get());
        }

        protected override void Awake()
        {
            base.Awake();

            _isHiddenHash = Animator.StringToHash(isHiddenTag);
        }

        private void Start()
        {
            DataContext.Value.Next.ObserveEveryValueChanged(x => x.Value).Subscribe(OnNextChanged).AddTo(Disposables);
        }

        private void OnNextChanged(BallViewModel ball)
        {
            animator.SetBool(_isHiddenHash, ball == null);
        }
    }
}