using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class MantisState : MonsterState<Mantis>
    {
        protected MantisFsm fsm => instance.fsm;
        protected MantisData data => instance.data;
        protected Mantis mantis => instance;
        // protected MantisFsm fsm => instance.fsm;

        public MantisState(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.senseData.UpdateData(mantis);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

            // NOTE: 테스트 로직.
            else if(Input.GetKeyDown(KeyCode.F1))
            {
                fsm.Change(fsm.idle);
                return true;
            }
            else if(Input.GetKeyDown(KeyCode.F2))
            {
                fsm.Change(fsm.walkFront);
                return true;
            }
            else if(Input.GetKeyDown(KeyCode.F3))
            {
                fsm.Change(fsm.walkBack);
                return true;
            }

            return false;
        }
    }
}