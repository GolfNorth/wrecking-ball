using System;
using Game.Presentation.Views;
using UniRx;
using UnityEngine;

namespace Game.Presentation.Components
{
    public class BaseViewComponent<TView, TViewModel> : MonoBehaviour where TView : BaseView<TViewModel>
    {
        [SerializeField]
        private TView view;

        protected readonly CompositeDisposable Disposables = new();

        protected TView View => view;

        protected TViewModel DataContext => view.DataContext.Value;

        protected virtual void Awake()
        {
            view.DataContext.ObserveEveryValueChanged(x => x.Value).Subscribe(OnDataContextChanged);
        }

        private void OnDestroy()
        {
            Disposables.Clear();
        }

        protected virtual void OnDataContextChanged(TViewModel dataContext)
        {
            if (dataContext == null)
                return;

            OnDataContextChanged();
        }

        protected virtual void OnDataContextChanged()
        {
        }
    }
}