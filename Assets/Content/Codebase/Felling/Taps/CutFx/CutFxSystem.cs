using System;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common.LifetimeData;
using Woodman.Felling.Tree;

namespace Woodman.Felling.Taps.CutFx
{
    [EcsSystem(typeof(CoreModule))]
    public class CutFxSystem : IRunSystem
    {
        private FellingCharacterController _characterController;
        private CutFxPool _cutFxPool;
        private DataWorld _world;
        private TreePiecesRepository _piecesRepository;
        private VisualSettings _visualSettings;

        public void Run()
        {
            var q = _world.Select<CutEvent>();
            if (q.Any())
            {
                var fxTransform = CreateFx();
                _world.NewEntity()
                    .AddComponent(new CutFxComponent { fxTransform = fxTransform })
                    .AddComponent(new Lifetime { remain = _cutFxPool.lifetime });
            }

            var fxQ = _world.Select<CutFxComponent>()
                .Without<Lifetime>();
            foreach (var entity in fxQ.GetEntities())
            {
                Debug.Log("[TEST] Destroy fx");
                _cutFxPool.Return(entity.GetComponent<CutFxComponent>().fxTransform);
                entity.Destroy();
            }
        }

        private Transform CreateFx()
        {
            var fxTransform = _cutFxPool.Get();
            var yRot = _characterController.CurrentFellingSide == FellingSide.Right ? -90 : 90;
            fxTransform.localRotation = Quaternion.Euler(0, yRot, 0);
            fxTransform.position = _cutFxPool.transform.position;
            return fxTransform;
        }
    }
}