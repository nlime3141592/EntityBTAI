namespace UnchordMetroidvania
{
    public class MantisChop : MantisAttack
    {
        public MantisChop(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        public override bool CanAttack()
        {
            return true;
        }
    }
}