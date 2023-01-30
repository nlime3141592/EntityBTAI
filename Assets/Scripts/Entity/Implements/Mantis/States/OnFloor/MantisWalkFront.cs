namespace UnchordMetroidvania
{
    public class MantisWalkFront : MantisWalk
    {
        public MantisWalkFront(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = mantis.axisInput.x * mantis.moveDir.x * data.walkSpeed;
            float vy = mantis.axisInput.x * mantis.moveDir.y * data.walkSpeed - 0.1f;

            mantis.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(mantis.senseData.bOnWallFront)
            {
                fsm.Change(fsm.idle);
                return true;
            }
            return false;
        }
    }
}