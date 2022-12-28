using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public interface IEntityLookConfig : IConfigurationBT, IEntityConfig
    {
        IEntityLookConfig lookConfig { get; }

        bool bFixLookDirX { get; set; }
        bool bFixLookDirY { get; set; }

        int lookDirX { get; set; }
        int lookDirY { get; set; }

        void UpdateLookDir();
    }
}