using System;

namespace Unchord
{
    public interface ISkill
    {
        void OnTriggerSkill(BattleModule2 _module);
    }

    public interface IAttackSkill : ISkill
    {
        float baseDamage { get; }
    }

    public interface IHealSkill : ISkill
    {

    }

    public interface IDefenceSkill : ISkill
    {

    }

    public static class SkillExtension
    {
        public static T_Skill GetInterface<T_Skill>(this IStateBase _state)
        where T_Skill : class, ISkill
        {
            if(_state is T_Skill)
                return _state as T_Skill;
            else
                throw new NullReferenceException("invalid state.");
        }

        public static bool TryGetInterface<T_Skill>(this IStateBase _state, out T_Skill _interface)
        where T_Skill : class, ISkill
        {
            if(_state is T_Skill)
            {
                _interface = _state as T_Skill;
                return true;
            }
            else
            {
                _interface = null;
                return false;
            }
        }
    }
}