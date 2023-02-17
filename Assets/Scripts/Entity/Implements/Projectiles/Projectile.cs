using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [RequireComponent(typeof(BattleModule))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Projectile : EntityBase
    {
        [HideInInspector] public BattleModule battleModule;
        [HideInInspector] public CircleCollider2D cCol;
        public int cntBounce = 1;
        public int CURRENT_STATE = -1;

        protected override void Start()
        {
            base.Start();

            TryGetComponent<BattleModule>(out battleModule);
            TryGetComponent<CircleCollider2D>(out cCol);

            gameObject.SetActive(false);

            cCol.enabled = false;
            vm.FreezePosition(true, true);

            aController.ChangeAnimation(0);
        }

        protected override void Update()
        {
            base.Update();

            CURRENT_STATE = aController.id;

            // if(aController.id == 1) Destroy(this.gameObject);
        }

        public void Ignore(Collider2D other, Vector2 velocity)
        {
            Physics2D.IgnoreCollision(cCol, other, true);

            gameObject.SetActive(true);

            cCol.enabled = true;
            vm.FreezePosition(false, false);
            vm.SetVelocityXY(velocity.x, velocity.y);
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if(cntBounce > 0)
                --cntBounce;

            if(cntBounce == 0)
            {
                vm.FreezePosition(true, false);
                vm.SetVelocityY(0);
                aController.ChangeAnimation(1);
            }
        }
    }
}