using UnityEngine;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Logs;
using Woodman.MetaInteractions;
using Woodman.MetaTrees;
using Woodman.Player.Indicators;
using Woodman.Player.Movement;
using Woodman.PlayerRes;
using Zenject;

namespace Woodman
{
    public class MainInstaller : BaseInstaller
    {
        [SerializeField]
        private MainViewProvider _mainViewProvider;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BuildingInteraction>().AsSingle();

            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InteractionsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LogsInteraction>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainViewProvider>().FromInstance(_mainViewProvider).AsSingle();
            Container.BindInterfacesAndSelfTo<MetaCoreTransition>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerResRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerResHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<TreeInteraction>().AsSingle();
            BindView(_mainViewProvider);
        }
    }
}