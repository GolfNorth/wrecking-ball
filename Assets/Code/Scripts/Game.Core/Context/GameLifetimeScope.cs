using Game.Infrastructure;
using VContainer;
using VContainer.Unity;

namespace Game.Context
{
    /// <summary>
    /// Общий игровой контекст
    /// </summary>
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<HighScoreSystem>(Lifetime.Singleton).As<IHighScoreSystem>();
        }
    }
}