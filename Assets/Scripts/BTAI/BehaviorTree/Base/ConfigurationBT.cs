using System;
using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public class ConfigurationBT : IConfigurationBT
    {
        public long curFps { get; set; } = -1;

        void IConfigurationBT.ClearFps()
        {
            curFps = -1;
        }

        void IConfigurationBT.AddFps()
        {
            ++curFps;
        }

        void IConfigurationBT.SetFps(long fps)
        {
            curFps = fps;
        }
    }
}