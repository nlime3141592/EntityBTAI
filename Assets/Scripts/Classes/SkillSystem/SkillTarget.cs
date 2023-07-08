namespace Unchord
{
    public class SkillTarget
    {
        public Entity target;
        public float innerCooltime;

        public SkillTarget(Entity _target, float _innerCooltime)
        {
            target = _target;
            innerCooltime = _innerCooltime;
        }
    }
}