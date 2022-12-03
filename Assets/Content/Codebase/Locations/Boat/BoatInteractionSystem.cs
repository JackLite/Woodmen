using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.Common.Tweens;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;
using Woodman.Logs.LogsUsing;
using Woodman.Player.PlayerResources;
using Woodman.Progress;
using Woodman.Settings;
using Woodman.Utils;

namespace Woodman.Locations.Boat
{
    [EcsSystem(typeof(MetaModule))]
    public class BoatInteractionSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerLogsRepository _resRepository;
        private BoatSaveService _boatSaveService;
        private PoolsProvider _poolsProvider;
        private ProgressionService _progressionService;
        private BuildingService _buildingService;
        private EcsOneData<LocationData> _locationData;
        private VisualSettings _visualSettings;
        private CharacterLogsView _characterLogsView;

        public void Run()
        {
            var q = _world.Select<Interact>()
                .Where<Interact>(c => c.interactType == InteractTypeEnum.Boat);
            if (!q.TrySelectFirst(out Interact interact))
                return;

            OnInteract(interact.target);
        }

        private void OnInteract(InteractTarget target)
        {
            var interact = target.GetComponent<BuildingInteract>();
            if (interact == null)
                return;

            if (_boatSaveService.IsBoatFinished(_progressionService.GetLocationIndex(), interact.BuildingView))
                return;

            Process(interact);
        }

        private void Process(BuildingInteract interact)
        {
            var locationIndex = _progressionService.GetLocationIndex();
            var currentState = _boatSaveService.GetState(locationIndex);
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
            var locationIndex = _progressionService.GetLocationIndex();
            var currentLogs = _boatSaveService.GetLogs(locationIndex);
            var currentState = _boatSaveService.GetState(locationIndex);
            var nextStateLogs = interact.BuildingView.GetResForState(currentState + 1);
            var needLogs = nextStateLogs - currentLogs;

            endLogs = 0;
            endState = currentState;
            var playerLogs = _resRepository.GetPlayerRes();
            if (needLogs <= playerLogs)
            {
                endLogs = nextStateLogs;
                endState += 1;
                _boatSaveService.SetState(locationIndex, currentState + 1);
                _boatSaveService.SetLogs(locationIndex, 0);
            }
            else
            {
                endLogs = currentLogs + playerLogs;
                _boatSaveService.SetLogs(locationIndex, endLogs);
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
                createEvent.onAfter = () =>
                {
                    var stateEvent = new BuildingChangeStateEvent
                    {
                        buildingView = interact.BuildingView,
                        newState = newState
                    };
                    if (newState == interact.BuildingView.StatesCount - 1)
                        stateEvent.onFinishBuilding = FinishLocation;

                    if (newState < interact.BuildingView.StatesCount - 1)
                    {
                        var locationIndex = _progressionService.GetLocationIndex();
                        var currentState = _boatSaveService.GetState(locationIndex);
                        var nextStateLogs = interact.BuildingView.GetResForState(currentState + 1);
                        stateEvent.nextStateLogs = nextStateLogs;
                    }

                    _world.CreateEvent(stateEvent);
                };
            }

            _world.NewEntity().AddComponent(createEvent);
        }

        private void FinishLocation()
        {
            _world.NewEntity().AddComponent(new NextLocationEvent());
        }
    }
}