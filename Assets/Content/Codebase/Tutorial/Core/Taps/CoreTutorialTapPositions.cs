using UnityEngine;

namespace Woodman.Tutorial.Core.Taps
{
    public class CoreTutorialTapPositions : MonoBehaviour
    {
        public RectTransform left;
        public RectTransform right;

        public Vector2 LeftAnchor => left.anchoredPosition;
        public Vector2 RightAnchor => right.anchoredPosition;
    }
}