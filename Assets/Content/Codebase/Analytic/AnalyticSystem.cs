using System.Collections.Generic;
using System.Globalization;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.Finish;
using Woodman.Felling.Start;
using Woodman.Felling.Tree;
using Woodman.Progress;

namespace Woodman.Analytic
{
    [EcsSystem(typeof(AnalyticModule))]
    public class AnalyticSystem : IPreInitSystem, IPostRunSystem
    {
        private AnalyticSenderFacade _analyticSender;
        private DataWorld _world;
        private ProgressionService _progressionService;
        private TreePiecesRepository _piecesRepository;

        public void PreInit()
        {
            AppMetrica.Instance.SendEventsBuffer();
            if (AnalyticHelper.IsLevelStarted())
            {
                var fields = CreateCommonFields();
                fields["result"] = AnalyticHelper.GetFinishReason(FellingFinishReason.GameClosed);
                var levelNumber = _progressionService.GetCurrentTreeNumber();
                fields["level_number"] = levelNumber.ToString(CultureInfo.InvariantCulture);
                _analyticSender.SendEvent("level_finish", fields);
                AnalyticHelper.RegisterFinishFelling();
            }
        }

        public void PostRun()
        {
            CheckLevelStart();
            CheckLevelFinish();
        }

        private void CheckLevelStart()
        {
            var q = _world.Select<StartFellingSignal>();
            if (!q.Any())
                return;

            q.DestroyAll();
            AnalyticHelper.RegisterStartFelling();
            var fields = CreateCommonFields();
            var levelNumber = _progressionService.GetCurrentTreeNumber();
            fields["level_number"] = levelNumber.ToString(CultureInfo.InvariantCulture);
            _analyticSender.SendEvent("level_start", fields);
        }

        private void CheckLevelFinish()
        {
            var q = _world.Select<FellingFinishSignal>();
            if (!q.TrySelectFirst<FellingFinishSignal>(out var signal))
                return;

            q.DestroyAll();
            AnalyticHelper.RegisterFinishFelling();
            var fields = CreateCommonFields();
            var levelNumber = _progressionService.GetCurrentTreeNumber();
            if (signal.reason == FellingFinishReason.Win)
                levelNumber--;
            fields["level_number"] = levelNumber.ToString(CultureInfo.InvariantCulture);
            fields["result"] = AnalyticHelper.GetFinishReason(signal.reason);
            fields["progress"] = ((int)(signal.progress * 100)).ToString(CultureInfo.InvariantCulture);
            fields["time"] = AnalyticHelper.GetFellingTime().ToString(CultureInfo.InvariantCulture);
            fields["continue"] = signal.secondChanceShowed ? "1" : "0";
            _analyticSender.SendEvent("level_finish", fields);
        }

        private Dictionary<string, string> CreateCommonFields()
        {
            var diff = AnalyticHelper.GetDiff(_progressionService.GetLastDifficult());
            var levelCount = AnalyticHelper.GetLevelCount().ToString(CultureInfo.InvariantCulture);
            var fields = new Dictionary<string, string>
            {
                { "level_count", levelCount },
                { "level_diff", diff }
            };
            return fields;
        }
    }
}