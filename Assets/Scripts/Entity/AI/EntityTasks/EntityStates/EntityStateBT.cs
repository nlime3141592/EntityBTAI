namespace UnchordMetroidvania
{
    public abstract class EntityStateBT<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        protected readonly AnimationController aController;

        private readonly EntityStateBegin<T> evStateBegin;
        private readonly EntityAnimationBegin<T> evAnimationBegin;
        private readonly EntityActionBegin<T> evActionBegin;
        private readonly EntityActionEnd<T> evActionEnd;
        private readonly EntityAnimationEnd<T> evAnimationEnd;
        private readonly EntityOnUpdate<T> evOnUpdate;
        private readonly EntityOnFixedUpdate<T> evOnFixedUpdate;
        private readonly EntityStateEnd<T> evStateEnd;

        private readonly SequenceNodeBT<T> seqMain;
        private readonly SequenceNodeBT<T> seqEvents;
        private readonly SequenceNodeBT<T> seqLogic;

        private readonly IfElseNodeBT<T> ifelse_seqEvents;

        private readonly EntityBeginOfAnimation<T> evcndAnimationBegin;
        private readonly EntityBeginOfAction<T> evcndActionBegin;
        private readonly EntityEndOfAction<T> evcndActionEnd;
        private readonly EntityEndOfAnimation<T> evcndAnimationEnd;

        private readonly IfOnlyNodeBT<T> ifonly_evcndAnimationBegin;
        private readonly IfOnlyNodeBT<T> ifonly_evcndActionBegin;
        private readonly IfOnlyNodeBT<T> ifonly_evcndActionEnd;
        private readonly IfOnlyNodeBT<T> ifonly_evcndAnimationEnd;

        private readonly SelectorNodeBT<T> sel_evcndAnimationBegin;
        private readonly SelectorNodeBT<T> sel_evcndActionBegin;
        private readonly SelectorNodeBT<T> sel_evcndActionEnd;
        private readonly SelectorNodeBT<T> sel_evcndAnimationEnd;

        private readonly IfOnlyNodeBT<T> ifonly_seqLogic;

        private readonly InverterNodeBT<T> inv_evOnUpdate;

        private readonly FailureNodeBT<T> constF;
        private readonly RunningNodeBT<T> constR;
        private readonly SuccessNodeBT<T> constS;

        public EntityStateBT(T instance, AnimationController aController)
        : base(instance)
        {
            this.aController = aController;

            evStateBegin = new EntityStateBegin<T>(instance, this);
            evAnimationBegin = new EntityAnimationBegin<T>(instance, this);
            evActionBegin = new EntityActionBegin<T>(instance, this);
            evActionEnd = new EntityActionEnd<T>(instance, this);
            evAnimationEnd = new EntityAnimationEnd<T>(instance, this);
            evOnUpdate = new EntityOnUpdate<T>(instance, this);
            evOnFixedUpdate = new EntityOnFixedUpdate<T>(instance, this);
            evStateEnd = new EntityStateEnd<T>(instance, this);

            seqMain = BehaviorTree.Sequence<T>(instance, 3);
            seqEvents = BehaviorTree.Sequence<T>(instance, 4);
            seqLogic = BehaviorTree.Sequence<T>(instance, 2);

            ifelse_seqEvents = BehaviorTree.IfElse<T>(instance);

            evcndAnimationBegin = new EntityBeginOfAnimation<T>(instance, aController);
            evcndActionBegin = new EntityBeginOfAction<T>(instance, aController);
            evcndActionEnd = new EntityEndOfAction<T>(instance, aController);
            evcndAnimationEnd = new EntityEndOfAnimation<T>(instance, aController);

            ifonly_evcndAnimationBegin = BehaviorTree.IfOnly<T>(instance);
            ifonly_evcndActionBegin = BehaviorTree.IfOnly<T>(instance);
            ifonly_evcndActionEnd = BehaviorTree.IfOnly<T>(instance);
            ifonly_evcndAnimationEnd = BehaviorTree.IfOnly<T>(instance);

            sel_evcndAnimationBegin = BehaviorTree.Selector<T>(instance, 2);
            sel_evcndActionBegin = BehaviorTree.Selector<T>(instance, 2);
            sel_evcndActionEnd = BehaviorTree.Selector<T>(instance, 2);
            sel_evcndAnimationEnd = BehaviorTree.Selector<T>(instance, 2);

            ifonly_seqLogic = BehaviorTree.IfOnly<T>(instance);

            inv_evOnUpdate = BehaviorTree.Inverter<T>(instance);

            constF = BehaviorTree.Failure<T>(instance);
            constR = BehaviorTree.Running<T>(instance);
            constS = BehaviorTree.Success<T>(instance);

            seqMain[0] = evStateBegin;
            seqMain[1] = ifelse_seqEvents;
            seqMain[2] = evStateEnd;

            seqEvents[0] = sel_evcndAnimationBegin;
            seqEvents[1] = sel_evcndActionBegin;
            seqEvents[2] = sel_evcndActionEnd;
            seqEvents[3] = sel_evcndAnimationEnd;

            seqLogic[0] = inv_evOnUpdate;
            seqLogic[1] = evOnFixedUpdate;

            ifelse_seqEvents[0] = seqEvents;
            ifelse_seqEvents[1] = constR;
            ifelse_seqEvents[2] = constS;

            ifonly_evcndAnimationBegin[0] = evcndAnimationBegin;
            ifonly_evcndAnimationBegin[1] = evAnimationBegin;
            sel_evcndAnimationBegin[0] = ifonly_seqLogic;
            sel_evcndAnimationBegin[1] = ifonly_evcndAnimationBegin;

            ifonly_evcndActionBegin[0] = evcndActionBegin;
            ifonly_evcndActionBegin[1] = evActionBegin;
            sel_evcndActionBegin[0] = ifonly_seqLogic;
            sel_evcndActionBegin[1] = ifonly_evcndActionBegin;

            ifonly_evcndActionEnd[0] = evcndActionEnd;
            ifonly_evcndActionEnd[1] = evActionEnd;
            sel_evcndActionEnd[0] = ifonly_seqLogic;
            sel_evcndActionEnd[1] = ifonly_evcndActionEnd;

            ifonly_evcndAnimationEnd[0] = evcndAnimationEnd;
            ifonly_evcndAnimationEnd[1] = evAnimationEnd;
            sel_evcndAnimationEnd[0] = ifonly_seqLogic;
            sel_evcndAnimationEnd[1] = ifonly_evcndAnimationEnd;

            ifonly_seqLogic[0] = seqLogic;
            ifonly_seqLogic[1] = constR;

            inv_evOnUpdate[0] = evOnUpdate;
        }

        protected sealed override InvokeResult p_Invoke()
        {
            return seqMain.Invoke();
        }

        public virtual void OnStateBegin() {}
        public virtual InvokeResult OnUpdate() { return InvokeResult.Failure; }
        public virtual void OnFixedUpdate() {}
        public virtual void OnAnimationBegin() {}
        public virtual void OnActionBegin() {}
        public virtual void OnActionEnd() {}
        public virtual void OnAnimationEnd() {}
        public virtual void OnStateEnd() {}
    }
}