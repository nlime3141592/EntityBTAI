namespace Unchord
{
    public interface IEntityAggressionEvents : IStateEventListener
    {
        void OnAggroBegin(SEH_EntityAggression _aggModule);
        void OnAggressive(SEH_EntityAggression _aggModule);
        void OnAggroEnd(SEH_EntityAggression _aggModule);
    }
}