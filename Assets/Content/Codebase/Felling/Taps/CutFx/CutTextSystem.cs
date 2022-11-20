using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Common.Tweens;
using Random = UnityEngine.Random;

namespace Woodman.Felling.Taps.CutFx
{
    [EcsSystem(typeof(CoreModule))]
    public class CutTextSystem : IRunSystem
    {
        private CutTextPool _textPool;
        private DataWorld _world;
        public void Run()
        {
            var q = _world.Select<CutEvent>();
            if (!q.TrySelectFirst(out CutEvent cutEvent))
                return;

            var text = _textPool.Get();
            var startPos = text.transform.localPosition;
            var x = Random.Range(-text.xAmplitude, text.xAmplitude);
            var endPos = startPos + Vector3.up * text.endY + Vector3.right * x;
            var zRot = Random.Range(0, text.angleAmplitude);
            zRot *= x > 0 ? 1 : -1;
            var endRotation = Quaternion.Euler(0, 0, zRot);
            var startRotation = text.transform.localRotation;
            text.SetCount(cutEvent.size);
            var tween = new TweenData
            {
                remain = _textPool.lifetime,
                update = r =>
                {
                    var normalizedR = (_textPool.lifetime - r) / _textPool.lifetime;
                    var opacity = math.lerp(1, text.endOpacity, normalizedR);
                    text.SetOpacity(opacity);
                    var pos = Vector3.Lerp(startPos, endPos, normalizedR);
                    text.transform.localPosition = pos;
                    var rotation = Quaternion.Lerp(startRotation, endRotation, normalizedR);
                    text.transform.localRotation = rotation;
                },
                validate = () => text != null,
                onEnd = () => _textPool.Return(text)
            };
            _world.NewEntity().AddComponent(tween);
        }
    }
}