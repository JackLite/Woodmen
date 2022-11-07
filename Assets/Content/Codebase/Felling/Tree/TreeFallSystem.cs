using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling.Taps;

namespace Woodman.Felling.Tree
{
    [EcsSystem(typeof(CoreModule))]
    public class TreeFallSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<TreePiecesData> _piecesData;
        private TreePiecesRepository _piecesRepository;
        private VisualSettings _visualSettings;

        public void Run()
        {
            ref var pd = ref _piecesData.GetData();
            var q = _world.Select<CutEvent>();
            var y = 0f;
            foreach (var entity in q.GetEntities())
            {
                y += _visualSettings.pieceHeight;
                pd.remainY += _visualSettings.pieceHeight;
                entity.Destroy();
            }

            if (pd.remainY == 0)
                return;
            var fallDelta = _visualSettings.fallSpeed * Time.deltaTime;
            pd.remainY -= fallDelta;
            if (pd.remainY < 0) pd.remainY = 0;
            foreach (var treePiece in _piecesRepository.GetPieces())
            {
                var oldPos = treePiece.transform.position;
                treePiece.TargetY -= y;
                var newY = oldPos.y - fallDelta;
                if (newY < treePiece.TargetY) newY = treePiece.TargetY;
                treePiece.transform.position = new Vector3(oldPos.x, newY, oldPos.z);
            }
        }
    }
}