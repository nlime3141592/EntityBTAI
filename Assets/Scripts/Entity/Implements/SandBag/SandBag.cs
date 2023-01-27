namespace UnchordMetroidvania
{
    public class SandBag : EntityBase
    {
        public SandBagHard stateHard;
        public SandBagSoft stateSoft;

        public SandBagState currentState = null;
        public InvokeResult currentInvocation = InvokeResult.Failure;
        public bool value = false;

        protected override void Start()
        {
            base.Start();

            aController = GetComponent<AnimationController>();

            stateHard = new SandBagHard(this, aController);
            stateSoft = new SandBagSoft(this, aController);
            currentState = stateSoft;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if(currentState == null)
                return;

            currentInvocation = currentState.Invoke();
        }
    }
}