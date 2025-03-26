using Game.Data;
using Game.Infrastructure;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Context
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameSettingsData gameSettingsData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(gameSettingsData.CreateSettings()).As<GameSettings>();
            builder.Register<TowerFactory>(Lifetime.Singleton).As<ITowerFactory>();
            builder.Register<BallFactory>(Lifetime.Singleton).As<IBallFactory>();
            builder.RegisterEntryPoint<GameManager>();
        }
    }
}