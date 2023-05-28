using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public interface ISkillEvent : IStateEventListener
    {
        void OnSkill(SkillModule _skModule);
    }
}