using Unity.Mathematics;
using Woodman.Felling.Settings;
using Random = UnityEngine.Random;

namespace Woodman.Felling.Tree.Generator
{
    public class TreePieceBranchModGenerator
    {
        private readonly TreeElementSettings _timeFreezeSettings;
        private readonly TreeElementSettings _restoreTimeSettings;
        private readonly TreeElementSettings _hiveSettings;

        private TreeGeneratorPossibility _timeFreezePossibility;
        private TreeGeneratorPossibility _restoreTimePossibility;
        private TreeGeneratorPossibility _hivePossibility;

        public TreePieceBranchModGenerator(TreeGenerationSettings settings)
        {
            _timeFreezeSettings = settings.timeFreeze;
            _restoreTimeSettings = settings.restoreTime;
            _hiveSettings = settings.hive;
            
            _timeFreezePossibility = TreeGeneratorPossibilityFactory.Create(_timeFreezeSettings);
            _restoreTimePossibility = TreeGeneratorPossibilityFactory.Create(_restoreTimeSettings);
            _hivePossibility = TreeGeneratorPossibilityFactory.Create(_hiveSettings);
        }

        public void Reset()
        {
            _timeFreezePossibility = TreeGeneratorPossibilityFactory.Create(_timeFreezeSettings);
        }

        public BranchModEnum GenerateBranchMod(int pieceIndex, bool isBranchSameSide)
        {
            var typeR = Random.Range(0, 1f);
            var acc = 0f;
            if (Check(_timeFreezeSettings, ref _timeFreezePossibility, pieceIndex, typeR, ref acc))
                return BranchModEnum.TimeFreeze;
            if (Check(_restoreTimeSettings, ref _restoreTimePossibility, pieceIndex, typeR, ref acc))
                return BranchModEnum.RestoreTime;
            if (Check(_hiveSettings, ref _hivePossibility, pieceIndex, typeR, ref acc))
                return BranchModEnum.Hive;

            return BranchModEnum.None;
        }

        private static bool Check(
            TreeElementSettings element,
            ref TreeGeneratorPossibility possibility,
            int pieceIndex,
            float random,
            ref float acc)
        {
            var pieceDiff = pieceIndex - possibility.lastGeneratedPieceIndex;

            acc += possibility.possibility;
            if (element.afterIndex > pieceIndex)
                return false;

            RecalcPossibility(element, pieceDiff, ref possibility);

            if (!(random < acc))
                return false;

            possibility.possibility = element.startPossibility;
            possibility.lastGeneratedPieceIndex = pieceIndex;
            return true;
        }

        private static void RecalcPossibility(TreeElementSettings element, float pieceDiff,
            ref TreeGeneratorPossibility possibility)
        {
            possibility.possibility = element.startPossibility +
                                      pieceDiff * element.possibilityCoef;
            possibility.possibility =
                math.min(possibility.possibility, element.maxPossibility);
        }
    }
}