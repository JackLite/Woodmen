using UnityEngine;
using Zenject;

namespace Woodman.Felling
{
    public class FellingInstaller : MonoInstaller
    {
        [SerializeField]
        private FellingViewProvider _fellingViewProvider;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TreeContainer>()
                     .FromInstance(_fellingViewProvider.TreeContainer)
                     .AsSingle();
            Container.BindInterfacesAndSelfTo<TreeGenerator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FellingBinding>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TapController>()
                     .FromInstance(_fellingViewProvider.TapController)
                     .AsSingle();
            Container.BindInterfacesAndSelfTo<FellingPlayerController>()
                     .FromInstance(_fellingViewProvider.FellingPlayerController)
                     .AsSingle();
            Container.BindInterfacesAndSelfTo<Cutter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PiecesController>().AsSingle();
        }
    }
}