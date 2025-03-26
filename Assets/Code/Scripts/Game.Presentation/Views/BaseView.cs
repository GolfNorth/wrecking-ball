using UniRx;
using UnityEngine;

namespace Game.Presentation.Views
{
    /// <summary>
    /// Базовый класс визуализатора
    /// </summary>
    public abstract class BaseView<TViewModel> : MonoBehaviour
    {
        private readonly ReactiveProperty<TViewModel> _dataContext = new();

        protected readonly CompositeDisposable Disposables = new();

        public ReadOnlyReactiveProperty<TViewModel> DataContext { get; private set; }

        protected virtual void Awake()
        {
            DataContext = _dataContext.ToReadOnlyReactiveProperty();
        }

        public virtual void Init(TViewModel dataContext)
        {
            _dataContext.Value = dataContext;
        }

        protected virtual void OnDisable()
        {
            Disposables.Clear();
        }
    }
}