using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Woodman.Felling
{
    public class FellingCharacterController : MonoBehaviour
    {
        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private SideToPosMap[] _sideToPos;

        private Dictionary<Side, Tuple<Vector3, Quaternion>> _sideToPosMap;
        private static readonly int _cut = Animator.StringToHash("Cut");
        public Side CurrentSide { get; private set; }

        private void Awake()
        {
            _sideToPosMap = _sideToPos.ToDictionary(
                s => s.side, 
                s => new Tuple<Vector3, Quaternion>(s.pos, s.rotation));
        }

        public void MoveToSide(Side side)
        {
            _character.position = _sideToPosMap[side].Item1;
            _character.rotation = _sideToPosMap[side].Item2;
            CurrentSide = side;
        }

        public void Cut()
        {
            //_animator.SetTrigger(_cut);
        }

        [Serializable]
        private struct SideToPosMap
        {
            public Side side;
            public Vector3 pos;
            public Quaternion rotation;
        }
    }
}