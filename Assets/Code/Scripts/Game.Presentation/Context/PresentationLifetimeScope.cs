using Game.Presentation.Infrastructure;
using Game.Presentation.Views;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Game.Presentation.Context
{
    public class PresentationLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BallObjectPool>().As<IObjectPool<BallView>>();
        }
    }
}