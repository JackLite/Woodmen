using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;

namespace Woodman.Felling.Tree.Branches
{
    [EcsSystem(typeof(FellingModule))]
    public class BoosterSystem : IRunSystem
    {
        private DataWorld _world;
        
        public void Run()
        {
            var entities = _world.Select<BoosterCollideEvent>().GetEntities();
            foreach (var e in entities)
            {
                var boosterType = e.GetComponent<BoosterCollideEvent>().boosterType;
                ProcessBooster(boosterType);
                e.AddComponent(new EcsOneFrame());
            }
        }

        private void ProcessBooster(BoosterType boosterType)
        {
            switch (boosterType)
            {
                case BoosterType.TimeFreeze:
                    _world.NewEntity().AddComponent(new FellingTimeFreeze { unfreezeTime = Time.time + 5 });
                    break;
                case BoosterType.RestoreTime:
                    _world.NewEntity().AddComponent(new FellingRestoreTimeEvent());
                    break;
            }
        }
    }
}