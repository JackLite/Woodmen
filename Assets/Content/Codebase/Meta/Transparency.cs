using Woodman.Common;

namespace Woodman.Meta
{
    public struct Transparency
    {
        public float current;
        public float target;
        public float delta;
        public MeshTransparent transparent;
        public bool isFinish;
    }
}