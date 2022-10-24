using System.Linq;
using Unity.Mathematics;
using Woodman.Felling.Settings;
using Random = UnityEngine.Random;

namespace Woodman.Felling.Tree.Generator
{
    public class TreePieceTypeGenerator
    {
        private readonly TreeElementSettings _hollow;
        private readonly TreeStrongPieceSettings _strong;
        private TreeGeneratorPossibility _hollowPossibility;
        private TreeGeneratorPossibility _strongPossibility;

        public TreePieceTypeGenerator(in TreeGenerationSettings settings)
        {
            _hollow = settings.hollow;
            _strong = settings.strong;

            _hollowPossibility = TreeGeneratorPossibilityFactory.Create(settings.hollow);
            _strongPossibility = TreeGeneratorPossibilityFactory.Create(settings.strong);
        }

        public TreePieceType Generate(int pieceIndex)
        {
            var pieceDiff = pieceIndex - _hollowPossibility.lastGeneratedPieceIndex;

            var typeR = Random.Range(0, 1f);
            var acc = _hollowPossibility.possibility;
            if (_hollow.afterIndex <= pieceIndex)
            {
                RecalcPossibility(_hollow, pieceDiff, ref _hollowPossibility);
                if (typeR < acc)
                {
                    _hollowPossibility.possibility = _hollow.startPossibility;
                    _hollowPossibility.lastGeneratedPieceIndex = pieceIndex;
                    return TreePieceType.Hollow;
                }
            }

            acc += _strongPossibility.possibility;
            if (_strong.afterIndex <= pieceIndex)
            {
                RecalcPossibility(_hollow, pieceDiff, ref _strongPossibility);

                if (typeR < acc)
                {
                    _strongPossibility.possibility = _strong.startPossibility;
                    _strongPossibility.lastGeneratedPieceIndex = pieceIndex;
                    return TreePieceType.Strong;
                }
            }

            return TreePieceType.Usual;
        }

        private void RecalcPossibility(TreeElementSettings element, float pieceDiff,
            ref TreeGeneratorPossibility possibility)
        {
            possibility.possibility = element.startPossibility +
                                      pieceDiff * element.possibilityCoef;
            possibility.possibility =
                math.min(possibility.possibility, element.maxPossibility);
        }

        public int GenerateStrongSize()
        {
            var sumWeight = 0f;
            foreach (var countSetting in _strong.countWeight)
                sumWeight += countSetting.weight;

            var random = Random.Range(0, sumWeight);
            var acc = 0f;
            foreach (var countSetting in _strong.countWeight)
            {
                acc += countSetting.weight;
                if (acc >= random)
                    return countSetting.count;
            }

            return _strong.countWeight.Last().count;
        }
    }
}