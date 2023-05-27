namespace Unchord
{
    public interface IAnimationEvents : IStateEvent
    {
        void OnAnimationBegin();
        void OnActionBegin();
        void OnActionEnd();
        void OnAnimationEnd();
    }
}