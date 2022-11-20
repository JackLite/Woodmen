using ModulesFramework.Data;
using UnityEngine;

namespace Woodman.Utils
{
    public static class Extensions
    {
        /// <summary>
        ///     Works like Mathf.Clamp but for Vector2
        /// </summary>
        /// <seealso cref="Mathf.Clamp(float,float,float)" />
        /// <seealso cref="Vector2.Clamp(float, float)" />
        /// <returns>Clamped vector</returns>
        public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
        }

        /// <summary>
        ///     Works like Mathf.Clamp but for Vector2
        ///     Use same min/max for x and y axes
        /// </summary>
        /// <seealso cref="Mathf.Clamp(float,float,float)" />
        /// <seealso cref="Vector2.Clamp(Vector2, Vector2)" />
        /// <returns>Clamped vector</returns>
        public static Vector2 Clamp(this Vector2 v, float min, float max)
        {
            return new Vector2(Mathf.Clamp(v.x, min, max), Mathf.Clamp(v.y, min, max));
        }

        public static Vector3 ScreenToCanvasPosition(this Canvas canvas, Vector3 screenPosition)
        {
            var viewportPosition = new Vector3(screenPosition.x / Screen.width,
                screenPosition.y / Screen.height,
                0);
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }

        public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
        {
            var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.sizeDelta;
            return Vector3.Scale(viewportPosition, scale);
        }

        public static Vector3 MirrorZ(this Vector3 v)
        {
            return new Vector3(v.x, v.y, -v.z);
        }

        public static Vector3 MirrorX(this Vector3 v)
        {
            return new Vector3(-v.x, v.y, v.z);
        }

        public static Entity CreateEvent<T>(this DataWorld world) where T : struct
        {
            return world.NewEntity().AddComponent(new T());
        }
        
        public static Entity CreateEvent<T>(this DataWorld world, T c) where T : struct
        {
            return world.NewEntity().AddComponent(c);
        }
    }
}