using CameraProcessing;
using DefaultNamespace;
using MetaInteractions;
using MetaTrees;
using Movement;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private MetaViewProvider _metaViewProvider;

    public override void InstallBindings()
    {
        Container.Bind<MetaViewProvider>().FromInstance(_metaViewProvider).AsSingle();
        Container.Bind<CamerasContainer>().FromInstance(_metaViewProvider.CamerasContainer).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMovementController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InteractionsController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TreeInteraction>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();
        Container.BindInterfacesAndSelfTo<WindowsSwitcher>().FromInstance(_metaViewProvider.WindowsSwitcher).AsSingle();
    }
}