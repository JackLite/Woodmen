using System;

namespace Woodman.Felling
{
    public class FellingEventBus
    {
        public Action<bool> OnEscapeFelling;
    }
}