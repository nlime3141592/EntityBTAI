namespace Unchord
{
    public interface IEntityAggressionEvents : IStateEvent
    {
        void OnAggroBegin(SET_EntityAggression _aggModule);
        void OnAggressive(SET_EntityAggression _aggModule);
        void OnAggroEnd(SET_EntityAggression _aggModule);
    }
}