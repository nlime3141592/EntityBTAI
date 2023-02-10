using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public interface IBattleState
    {
        EntityBase attacker { get; }
        List<EntityBase> targets { get; }
        LTRB range { get; }
        int targetCount { get; }
        float baseDamage { get; }
    }
}