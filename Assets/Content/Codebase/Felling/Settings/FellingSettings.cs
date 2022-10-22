using System;

namespace Woodman.Felling.Settings
{
    [Serializable]
    public struct FellingSettings
    {
        public float time;
        public float timeForCut;
        public TreeGenerationSettings treeGeneration;
    }
}