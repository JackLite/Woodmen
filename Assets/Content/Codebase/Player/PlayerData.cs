using System;
using UnityEngine;

namespace Woodman.Player
{
    [Serializable]
    public struct PlayerData
    {
        public int maxWoodCount;
        public Vector3 metaPos;
        public bool interact;
        public Vector3 lastInteractPosition;
    }
}