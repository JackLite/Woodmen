using System.Collections.Generic;
using UnityEngine;

namespace Woodman.Cheats
{
    public enum SomeEnum
    {
        enum1, enum2, enum3
    }
    public struct DebugTestComponent
    {
        public int integer;
        public double d;
        public float f;
        public string testStr;
        public List<string> names;
        public List<List<int>> listOfList;
        public Dictionary<string, List<int>> someDict;
        public Dictionary<string, List<int>> someDict2;
        public Collider collider;
        public SomeEnum someEnum;
    }
}