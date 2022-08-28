using Woodman.Loading;

namespace Woodman
{
    public class LoadingInstaller : BaseInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadingController>().AsSingle().NonLazy();
        }
    }
}