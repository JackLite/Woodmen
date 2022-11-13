using System;
using System.Threading.Tasks;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Common;
using Woodman.Common.Tweens;
using Woodman.Felling.Tree;
using Woodman.Felling.Win;
using Woodman.Logs;
using Woodman.Player.PlayerResources;

namespace Woodman.Felling.End
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingReturnSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private FellingCharacterController _characterController;
        private FellingUIProvider _fellingUIProvider;
        private LogsHeapService _logsHeapService;
        private LogsHeapRepository _logsHeapRepository;
        private PlayerCoinsRepository _coinsRepository;
        private UiProvider _uiProvider;
        private EcsOneData<TreeModel> _treeModel;

        public void Init()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick += OnWinClick;
            _uiProvider.FellingWinWindow.OnX2BtnClick += OnX2Click;
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

        private void OnX2Click()
        {
            if (_coinsRepository.GetPlayerRes() < 10)
            {
                Debug.LogError("Not enough coins");
                return;
            }

            _coinsRepository.SubtractRes(10);
            ref var tm = ref _treeModel.GetData();
            foreach(var id in tm.createdHeaps)
                _logsHeapRepository.Remove(id);
            var oldSize = tm.size;
            var x2Count = tm.size * 2;
            tm.size = x2Count;
            _logsHeapService.SaveLogs(ref tm);

            _uiProvider.FellingWinWindow.HideX2();
            var tweenData = new TweenData
            {
                remain = .5f,
                update = r =>
                {
                    var nRemain = r / .5f;
                    var count = (int) math.lerp(oldSize, x2Count, 1 - nRemain);
                    _uiProvider.FellingWinWindow.SetLogsCount(count);
                },
                onEnd = () => _uiProvider.FellingWinWindow.SetLogsCount(x2Count),
                validate = () => _uiProvider.FellingWinWindow != null
            };
            _world.NewEntity().AddComponent(tweenData);
            
            _world.Select<WinX2TweenTag>().DestroyAll();
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
                _uiProvider.InnerLoadingScreen.Show();
                await Task.Delay(TimeSpan.FromSeconds(_uiProvider.InnerLoadingScreen.animDuration));
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