namespace Unchord
{
    public interface IAnimationEvents : IStateEventListener
    {
        void OnAnimationBegin();
        void OnActionBegin();
        void OnActionEnd();
        void OnAnimationEnd();
    }
}