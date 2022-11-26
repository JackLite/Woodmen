using UnityEngine;

namespace Woodman.Tutorial
{
    [CreateAssetMenu]
    public class TutorialSettings : ScriptableObject
    {
        public string tutorialObjectsLayer = "TutorialObject";

        public float branchXOffset = 150;
    }
}