using Core;
using EcsCore;
using UnityEngine;
using Woodman.Cheats.View;

namespace Woodman.Cheats.Systems
{
    [EcsSystem(typeof(CheatsModule))]
    public class ResetSaveSystem : IInitSystem, IDestroySystem
    {
        private DebugViewProvider _debugViewProvider;
        public void Init()
        {
            _debugViewProvider.ResetSaveBtn.onClick.AddListener(ResetSave);
        }

        public void Destroy()
        {
            _debugViewProvider.ResetSaveBtn.onClick.RemoveListener(ResetSave);
        }

        private void ResetSave()
        {
            PlayerPrefs.DeleteAll();
            _debugViewProvider.DebugMessageView.SetMsg("Reset success! Restart the game.");
        }
    }
}