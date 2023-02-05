using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisDie : MantisOnFloor
    {
        public MantisDie(Mantis instance, int id, string name)
        : base(instance, id, name)
        {

        }

        public override bool OnUpdate()
        {
            if(mantis.aController.bEndOfAnimation)
                fsm.End();

            return false;
        }

        public override void OnStateEnd()
        {
            mantis.bEndOfEntity = true;
        }
    }
}