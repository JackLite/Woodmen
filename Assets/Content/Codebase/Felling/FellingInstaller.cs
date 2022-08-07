using System.Reflection;
using UnityEngine;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingInstaller : BaseInstaller
    {
        [SerializeField]
        private FellingViewProvider _fellingViewProvider;

        [SerializeField]
        private TextAsset _fellingSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FellingController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FellingEventBus>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingProcessor>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingSettingsContainer>().AsSingle().WithArguments(_fellingSettings);
            Container.BindInterfacesAndSelfTo<FellingTimer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingUiProcessor>().AsSingle();

            Container.BindInterfacesAndSelfTo<TreeGenerator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TreePieceFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TreePiecesRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<TreeProgressService>().AsSingle();
            BindView(_fellingViewProvider);
        }
    }
}