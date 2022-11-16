using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModulesFrameworkUnity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Felling;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.TreeInteraction;
using Woodman.Meta;
using Woodman.Player.Movement.View;
using Woodman.Player.PlayerResources;
using Woodman.Utils;
using Object = UnityEngine.Object;

namespace Woodman
{
    public class MetaModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            if (SceneManager.GetActiveScene().name != "MainScene")
            {
                await Addressables.LoadSceneAsync("MainScene").Task;
            }
            
            var viewProvider = Object.FindObjectOfType<MetaViewProvider>(true);
            AddDependency(viewProvider);
            BindView(viewProvider);
            await viewProvider.PoolsProvider.LogsUsingPool.WarmUp(20);
            CreateOneData<PlayerMovementData>();

            if (Debug.isDebugBuild)
            {
                EcsWorldContainer.World.InitModule<CheatsModule, MetaModule>();
                EcsWorldContainer.World.ActivateModule<CheatsModule>();
            }
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