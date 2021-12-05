using Zenject;
using GameHub.Core.Security;

namespace GameHub.Core.Context
{
    /// <summary>
    /// Class <c>ContextInstaller</c> represents the GameHub.Core DI context 
    /// configuration implementation. Each game should implement a custom
    /// DI context configuration specific to the game and should be placed
    /// inside the <c>Context</c> folder within the game's root namespace.
    /// 
    /// Implementation Location: GameHub.Games.<GameNamespace>.Context
    /// </summary>
    public class ContextInstaller : MonoInstaller
    {
        /// <summary>
        /// Method <c>InstallBindings</c> is responsible for wiring up all
        /// depedencies available to the GameHub.Core namespace.
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IGameConfigLoader>().To<GameConfigLoader>().AsSingle();
            Container.Bind<IGameSettingsService>().To<GameSettingsService>().AsSingle();
            Container.Bind<IResourceLoader>().To<ResourceLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IUserInfoService>().To<UserInfoService>().AsSingle();
        }
    }
}