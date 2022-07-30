using UnityEngine;
using Woodman.Buildings;
using Woodman.CameraProcessing;
using Woodman.MetaInteractions;
using Woodman.MetaTrees;
using Woodman.Player.Indicators;
using Woodman.Player.Movement;
using Woodman.PlayerRes;
using Zenject;

namespace Woodman
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private MetaViewProvider _metaViewProvider;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BuildingInteraction>().AsSingle();

            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();
            Container.BindInterfacesAndSelfTo<CamerasContainer>().FromInstance(_metaViewProvider.CamerasContainer)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InteractionsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MetaViewProvider>().FromInstance(_metaViewProvider).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerResRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerIndicatorsController>()
                .FromInstance(_metaViewProvider.PlayerIndicatorsController).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<TreeInteraction>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowsSwitcher>().FromInstance(_metaViewProvider.WindowsSwitcher)
                .AsSingle();
        }
    }
}