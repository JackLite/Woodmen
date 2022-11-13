using System;

namespace Woodman.Common.Delay
{
    public struct DelayData
    {
        public Action delayedFun;
        public Func<bool> validate;
    }
}