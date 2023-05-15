namespace Unchord
{
    public interface IDrawGizmosEvent : IStateEvent
    {
        void OnDrawGizmos(bool _bShow);
    }
}