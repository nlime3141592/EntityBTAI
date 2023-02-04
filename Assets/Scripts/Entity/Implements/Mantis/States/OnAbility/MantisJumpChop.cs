namespace UnchordMetroidvania
{
    public class MantisJumpChop : MantisAttack
    {
        public MantisJumpChop(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        public override bool CanAttack()
        {
            return true;
        }
    }
}