using Woodman.Felling.Settings;

namespace Woodman.Felling.Tree.Generator
{
    public static class TreeGeneratorPossibilityFactory
    {
        public static TreeGeneratorPossibility Create(TreeElementSettings elementSetting)
        {
            return new TreeGeneratorPossibility
            {
                possibility = elementSetting.startPossibility,
                lastGeneratedPieceIndex = elementSetting.afterIndex
            };
        }
    }
}