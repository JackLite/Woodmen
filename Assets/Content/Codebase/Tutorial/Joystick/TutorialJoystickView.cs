using UnityEngine;

namespace Woodman.Tutorial.Joystick
{
    public class TutorialJoystickView : MonoBehaviour
    {
        public float delta = 150;
        public float time = .5f;
        public AnimationCurve easing;
        
        [SerializeField]
        private RectTransform _rectTransform;

        public void SetPosition(Vector3 pos)
        {
            _rectTransform.anchoredPosition = pos;
        }

        public Vector3 GetPos()
        {
            return _rectTransform.anchoredPosition;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}