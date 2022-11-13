﻿using System;

namespace Woodman.Common.Tweens
{
    public struct TweenData
    {
        public float remain;
        public Action<float> update;
        public Action onEnd;
        public Func<bool> validate;
    }
}