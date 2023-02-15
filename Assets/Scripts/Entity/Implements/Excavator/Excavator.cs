using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class Excavator : EntityMonster
    {
        public ExcavatorFsm fsm;
        public ExcavatorData data;
        public ExcavatorTerrainSenseData senseData;
    }
}