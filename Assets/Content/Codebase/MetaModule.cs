using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModulesFrameworkUnity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Settings;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;
using Woodman.FellingTransition;
using Woodman.MetaInteractions;
using Woodman.MetaInteractions.TreeInteraction;
using Woodman.Player.Movement.View;
using Woodman.PlayerRes;
using Object = UnityEngine.Object;

namespace Woodman
{
    public class MetaModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            AddDependency(new PlayerResRepository());
            CreateOneData<PlayerMovementData>();

            EcsWorldContainer.World.InitModule<FellingModule, MetaModule>();
            if (Debug.isDebugBuild)
            {
                EcsWorldContainer.World.InitModule<CheatsModule, MetaModule>();
                EcsWorldContainer.World.ActivateModule<CheatsModule>();
            }

            EcsWorldContainer.World.ActivateModule<MetaModule>();
            return Task.CompletedTask;
        }

        protected override Dictionary<Type, int> GetSystemsOrder()
        {
            return new Dictionary<Type, int>
            {
                { typeof(InteractionRegisterSystem), -100 },
                { typeof(TreeInteractionSystem), -10 }
            };
        }
    }
}