using System;
using System.Threading.Tasks;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common;
using Woodman.Locations.Trees;

namespace Woodman.Felling.End
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingReturnSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private MetaTreesRepository _treesRepository;
        private FellingCharacterController _characterController;
        private FellingUIProvider _fellingUIProvider;
        private UiProvider _uiProvider;

        public void Init()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick += OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick += OnLoseClick;
            _uiProvider.FellingLoseWindow.OnRestartClick += OnRestartClick;
            _uiProvider.PauseView.OnRestart += OnRestartClick;
        }

        private void OnWinClick()
        {
            _uiProvider.FellingUi.Hide();
            _uiProvider.FellingWinWindow.Hide();
            Switch();
        }

        private void OnLoseClick()
        {
            _uiProvider.FellingUi.Hide();
            _uiProvider.FellingLoseWindow.Hide();
            Switch();
        }

        private async void Switch()
        {
            try
            {
                _uiProvider.SwitchCoreScreen.Show();
                await Task.Delay(TimeSpan.FromSeconds(_uiProvider.SwitchCoreScreen.animDuration));
                _world.DestroyModule<CoreModule>();
                _world.InitModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void OnRestartClick()
        {
            _uiProvider.FellingLoseWindow.Hide();
            _uiProvider.FellingUi.Show();
            _fellingUIProvider.StartGameBtn.gameObject.SetActive(true);
            _fellingUIProvider.FellingTimerView.SetProgress(1);
            _fellingUIProvider.TreeUIProgress.SetProgress(1);
        }

        public void Destroy()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick -= OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick -= OnLoseClick;
            _uiProvider.FellingLoseWindow.OnRestartClick -= OnRestartClick;
            _uiProvider.PauseView.OnRestart -= OnRestartClick;
        }
    }
}