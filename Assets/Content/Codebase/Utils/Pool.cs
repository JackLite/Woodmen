using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Woodman.Utils
{
    public class Pool<T> : MonoBehaviour where T : Component
    {
        [SerializeField]
        private AssetReference _prefab;

        private readonly Stack<T> _pool = new();

        public async Task WarmUp(int size)
        {
            var startPos = Vector3.one * 50000;
            for (var i = 0; i < size; ++i)
            {
                var go = await Addressables.InstantiateAsync(_prefab, startPos, Quaternion.identity, transform).Task;
                var component = go.GetComponent<T>();
                ResetPoolObject(component);
                _pool.Push(component);
            }
        }

        public T Get()
        {
            if (_pool.Count != 0)
                return _pool.Pop().GetComponent<T>();

            var startPos = Vector3.one * 50000;
            var go = Addressables.InstantiateAsync(_prefab, startPos, Quaternion.identity, transform)
                .WaitForCompletion();
            var component = go.GetComponent<T>();
            OnBeforeGet(component);
            return component;
        }

        public void Return(T poolObject)
        {
            ResetPoolObject(poolObject);
            _pool.Push(poolObject);
        }

        protected virtual void ResetPoolObject(T _)
        {
        }

        protected virtual void OnBeforeGet(T _)
        {
        }

        private void OnDestroy()
        {
            foreach (var t in _pool)
            {
                Addressables.ReleaseInstance(t.gameObject);
            }

            _pool.Clear();
        }
    }
}