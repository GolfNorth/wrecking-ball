using Game.Presentation.Views;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Game.Presentation.Infrastructure
{
    /// <summary>
    /// Пул для создания шаров
    /// </summary>
    public class BallObjectPool : MonoBehaviour, IObjectPool<BallView>
    {
        [SerializeField]
        private BallView ballPrefab;

        private ObjectPool<BallView> _objectPool;

        private IObjectResolver _container;

        public int CountInactive => _objectPool.CountInactive;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _container = container;

            _objectPool = new ObjectPool<BallView>(
                CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject
            );
        }

        private BallView CreatePooledItem()
        {
            var view = Instantiate(ballPrefab);

            _container.InjectGameObject(view.gameObject);

            return view;
        }

        private void OnTakeFromPool(BallView view)
        {
            view.IsKinematic.Value = true;
            view.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(BallView view)
        {
            view.IsKinematic.Value = true;
            view.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(BallView view)
        {
            Destroy(view.gameObject);
        }

        #region IObjectPool

        public BallView Get()
        {
            return _objectPool.Get();
        }

        public PooledObject<BallView> Get(out BallView v)
        {
            return _objectPool.Get(out v);
        }

        public void Release(BallView element)
        {
            _objectPool.Release(element);
        }

        public void Clear()
        {
            _objectPool.Clear();
        }

        #endregion
    }
}