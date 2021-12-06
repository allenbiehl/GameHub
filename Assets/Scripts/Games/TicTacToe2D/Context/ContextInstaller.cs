using Zenject;
using GameHub.Core;
using GameHub.Core.Security;

namespace GameHub.Games.TicTacToe2D.Context
{
    /// <summary>
    /// Class <c>ContextInstaller</c> represents the GameHub.Games.TicTacToe2D
    /// DI context configuration implementation. 
    /// </summary>
    public class ContextInstaller : MonoInstaller
    {
        /// <summary>
        /// Method <c>InstallBindings</c> is responsible for wiring up all
        /// depedencies available to the GameHub.Games.TicTacToe2D namespace.
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IGameConfigLoader>().To<GameConfigLoader>().AsSingle();
            Container.Bind<IGameManager>().To<GameManager>().AsSingle();
            Container.Bind<IGameSettingsService>().To<GameSettingsService>().AsSingle();
            Container.Bind<IPlayerSettingsService>().To<PlayerSettingsService>().AsSingle();
            Container.Bind<IResourceLoader>().To<ResourceLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IUserInfoService>().To<UserInfoService>().AsSingle();
        }
    }
}