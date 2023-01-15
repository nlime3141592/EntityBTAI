using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class MantisData
    {
        [Header("Skill Options")]
        public BoxRangeBattleSkillOption upSlice;
        public BoxRangeBattleSkillOption downSlice;
        public BoxRangeBattleSkillOption backSlice;
        public BoxRangeBattleSkillOption jumpSlice;
    }
}