using System.Collections.Generic;

namespace Unchord
{
    public interface IBattleState
    {
        Entity attacker { get; }
        List<Entity> targets { get; }
        LTRB range { get; }
        int targetCount { get; }
        float baseDamage { get; }
    }
}