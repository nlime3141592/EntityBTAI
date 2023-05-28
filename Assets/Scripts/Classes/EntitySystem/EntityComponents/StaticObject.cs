using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/Entity Components/Static Object")]
    public class StaticObject : Entity
    {
        public const int c_st_IDLE = 0;
        public const int c_st_FREE_FALL = 1;
        public const int c_st_DIE = 2;

        public float speedMin_FreeFall = -13.0f;
        public float gravity = -49.5f;

        [HideInInspector] public StaticObjectTerrainSensor senseData;

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            senseData = new StaticObjectTerrainSensor();
        }

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<StaticObject> fsm = new StateMachine<StaticObject>(3);
            fsm.instance = this;

            fsm.Add(new StaticObjectIdle());
            fsm.Add(new StaticObjectFreeFall());
            fsm.Add(new StaticObjectDie());

            fsm.Begin(StaticObject.c_st_IDLE);
            return fsm;
        }
    }
}