using System;
using System.Collections.Generic;

namespace Unchord
{
    public abstract class TimerHandlerBase
    {
        public abstract void OnUpdate(float _deltaTime);
    }
}