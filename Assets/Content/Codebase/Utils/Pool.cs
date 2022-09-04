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
                _pool.Push(go.GetComponent<T>());
            }
        }

        public T Get()
        {
            if (_pool.Count != 0)
                return _pool.Pop().GetComponent<T>();

            var startPos = Vector3.one * 50000;
            var go = Addressables.InstantiateAsync(_prefab, startPos, Quaternion.identity, transform)
                                 .WaitForCompletion();
            return go.GetComponent<T>();
        }

        public void Return(T poolObject)
        {
            _pool.Push(poolObject);
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