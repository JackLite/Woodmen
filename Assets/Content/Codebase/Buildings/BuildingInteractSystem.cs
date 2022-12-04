using Cinemachine;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Common.Delay;
using Woodman.Common.Tweens;
using Woodman.Locations;
using Woodman.Locations.Boat;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;
using Woodman.Logs.LogsUsing;
using Woodman.Meta;
using Woodman.Player.Movement.View;
using Woodman.Player.PlayerResources;
using Woodman.Progress;
using Woodman.Settings;
using Woodman.Utils;

namespace Woodman.Buildings
{
    [EcsSystem(typeof(MetaModule))]
    public class BuildingInteractSystem : IRunSystem
    {
        private BoatSaveService _boatSaveService;
        private BuildingService _buildingService;
        private BuildingsRepository _buildingsRepository;
        private CharacterLogsView _characterLogsView;
        private DataWorld _world;
        private EcsOneData<LocationData> _locationData;
        private MovementView _movementView;
        private MetaViewProvider _viewProvider;
        private PlayerLogsRepository _resRepository;
        private ProgressionService _progressionService;
        private VisualSettings _visualSettings;

        public void Run()
        {
            var q = _world.Select<Interact>()
                .Where<Interact>(c => c.interactType == InteractTypeEnum.Building);
            if (!q.TrySelectFirst(out Interact interact))
                return;

            OnInteract(interact.target);
        }

        private void OnInteract(InteractTarget target)
        {
            var interact = target.GetComponent<BuildingInteract>();
            if (interact == null)
                return;

            if (_buildingsRepository.IsLastState(interact.BuildingView))
                return;

            if (_resRepository.GetPlayerRes() == 0)
                return;

            Process(interact);
        }

        private void Process(BuildingInteract interact)
        {
            var currentState = _buildingsRepository.GetBuildingStateIndex(interact.BuildingView.Id);
            var nextStateLogs = interact.BuildingView.GetResForState(currentState + 1);
            var currentLogs = ProcessLogic(interact, out var endLogs, out var endState);

            CreateUsingLogs(interact, endLogs, endState, currentState);

            var totalTime = _visualSettings.usingLogsTime +
                            _visualSettings.usingLogsCount * _visualSettings.usingLogsDelayBetween;
            var tweenData = new TweenData
            {
                remain = totalTime,
                update = r =>
                {
                    var logsCount = math.lerp(currentLogs, endLogs, 1 - r / totalTime);
                    interact.BuildingView.SetLogs((int)logsCount, nextStateLogs);
                },
                validate = () => interact.BuildingView != null,
                onEnd = () =>
                {
                    interact.BuildingView.SetLogs(endLogs, nextStateLogs);
                    _world.CreateOneFrame().AddComponent(new ChangeResEvent());
                }
            };
            _world.NewEntity().AddComponent(tweenData);
        }

        private int ProcessLogic(BuildingInteract interact, out int endLogs, out int endState)
        {
            var building = interact.BuildingView.Id;
            var currentLogs = _buildingsRepository.GetBuildingLogsCount(building);
            var currentState = _buildingsRepository.GetBuildingStateIndex(building);
            var nextStateLogs = interact.BuildingView.GetResForState(currentState + 1);
            var needLogs = nextStateLogs - currentLogs;

            endLogs = 0;
            endState = currentState;
            var playerLogs = _resRepository.GetPlayerRes();
            if (needLogs <= playerLogs)
            {
                endLogs = nextStateLogs;
                endState += 1;
                _buildingsRepository.SetBuildingStateIndex(endState, building);
                _buildingsRepository.SetBuildingLogsCount(0, building);
            }
            else
            {
                endLogs = currentLogs + playerLogs;
                _buildingsRepository.SetBuildingLogsCount(endLogs, building);
            }

            _resRepository.SubtractRes(math.min(playerLogs, needLogs));
            return currentLogs;
        }

        private void CreateUsingLogs(BuildingInteract interact, int logsCount, int newState, int oldState)
        {
            var createEvent = new UsingLogsCreateEvent
            {
                count = math.min(logsCount, _visualSettings.usingLogsCount),
                from = _characterLogsView.LogsTargetPos,
                to = () => interact.BuildingView.transform.position,
                delayBetween = _visualSettings.usingLogsDelayBetween
            };

            if (oldState != newState)
            {
                _movementView.ToggleMove(false);
                UpdateCamera(interact.BuildingView.transform, .75f);
                createEvent.onAfter = () =>
                {
                    var changeStateEvent = new BuildingChangeStateEvent
                    {
                        buildingView = interact.BuildingView,
                        newState = newState
                    };
                    changeStateEvent.onFinishBuilding = ReturnToChar;
                    if (interact.BuildingView.IsLastState(newState))
                    {
                        FinishBuilding();
                    }
                    else
                    {
                        var currentState = _buildingsRepository.GetBuildingStateIndex(interact.BuildingView.Id);
                        changeStateEvent.nextStateLogs = interact.BuildingView.GetResForState(currentState + 1);
                    }

                    _world.CreateEvent(changeStateEvent);
                };
            }

            _world.NewEntity().AddComponent(createEvent);
        }

        private void FinishBuilding()
        {
            _progressionService.RegisterFinishBuilding();
            if (_progressionService.IsBuildingsFinished())
            {
                var ld = _locationData.GetData();
                ld.locationView.ShowBoat();
                var locationIndex = _progressionService.GetLocationIndex();
                var boatState = _boatSaveService.GetState(locationIndex);
                ld.locationView.SetBoatState(boatState);
                var logs = _boatSaveService.GetLogs(locationIndex);
                ld.locationView.SetBoatLogs(logs, ld.locationView.GetBoatLogsForState(boatState + 1));
            }
        }
        
        private void ReturnToChar()
        {
            SetCameraTransform(_viewProvider.WoodmanContainer.transform);
            DelayedFactory.Create(_world, 0.75f, () =>
            {
                SetCameraDamping(0);
                _movementView.ToggleMove(true);
            });
        }

        private void UpdateCamera(Transform transform, float damping)
        {
            SetCameraDamping(damping);
            SetCameraTransform(transform);
        }

        private void SetCameraDamping(float damping)
        {
            var transposer = _viewProvider.MetaCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_XDamping = damping;
            transposer.m_YDamping = damping;
            transposer.m_ZDamping = damping;
        }

        private void SetCameraTransform(Transform transform)
        {
            _viewProvider.MetaCamera.Follow = transform;
            _viewProvider.MetaCamera.LookAt = transform;
        }
    }
}