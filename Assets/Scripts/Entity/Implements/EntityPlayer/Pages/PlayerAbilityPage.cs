using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerAbilityPage : PageNodeBT<EntityPlayer>
    {
        private SelectorNodeBT<EntityPlayer> sel;
        private PlayerJumpOnFloor jumpOnFloor;

        public PlayerAbilityPage(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            sel = BehaviorTree.Selector<EntityPlayer>(config, id, name, 1);
            jumpOnFloor = new PlayerJumpOnFloor(config, 400, "JumpOnFloor");

            int allocIndex = -1;
            sel.Alloc(++allocIndex, jumpOnFloor);
        }

        protected override InvokeResult p_Invoke()
        {
            bool bIsRun = config.instance.bIsRun;

            jumpOnFloor.speedStatX = bIsRun ? config.instance.runSpeed : config.instance.baseMoveSpeed;
            jumpOnFloor.jumpSpeedStatY = config.instance.jumpSpeedBase;
            jumpOnFloor.jumpGravityY = config.instance.jumpGravity;

            return sel.Invoke();
        }
    }
}