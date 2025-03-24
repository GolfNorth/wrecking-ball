using Game.Infrastructure;
using VContainer;
using VContainer.Unity;

namespace Game.Context
{
    /// <summary>
    /// Геймплейный контекст
    /// </summary>
    public class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}