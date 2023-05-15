using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public interface ISkillEvent : IStateEvent
    {
        void OnSkill(SkillModule _skModule);
    }
}