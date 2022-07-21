using System.Reflection;
using UnityEngine;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Misc;
using Zenject;

namespace Woodman.Felling
{
    public class FellingInstaller : MonoInstaller
    {
        [SerializeField]
        private FellingViewProvider _fellingViewProvider;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FellingController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FellingInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingProcessor>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingTimer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FellingUI>().AsSingle();
            Container.BindInterfacesAndSelfTo<TreeGenerator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TreePieceFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TreePiecesRepository>().AsSingle();
            BindView(_fellingViewProvider);
        }

        private void BindView<T>(T viewProvider) where T : class
        {
            var t = typeof(T);
            foreach (var p in t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            {
                if (p.GetCustomAttribute(typeof(ViewInjectAttribute)) == null)
                    continue;

                var val = p.GetValue(viewProvider);
                if (val == null)
                {
                    Debug.LogError($"{p.FieldType} not found in {viewProvider.GetType()}");
                    continue;
                }
                Bind(val);
            }
        }

        private void Bind(object t)
        {
            var type = t.GetType();
            Container.BindInterfacesAndSelfTo(type).FromInstance(t).AsSingle();
        }
    }
}