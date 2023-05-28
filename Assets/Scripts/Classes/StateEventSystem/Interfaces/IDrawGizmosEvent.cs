namespace Unchord
{
    public interface IDrawGizmosEvent : IStateEventListener
    {
        void OnDrawGizmos(bool _bShow);
    }
}