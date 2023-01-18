using UnityEngine;

namespace UnchordMetroidvania
{
    [RequireComponent(typeof(Player))]
    public class PlayerFSM : MonoBehaviour
    {
        public int stateID => currentState?.id ?? int.MinValue;
        public string stateName => currentState?.name ?? "NONE";
        public int totalFps { get; private set; } = -1;
        public int fps { get; private set; } = -1;
        public int nextFps => (fps + 1);

        private Player m_player;
        private PlayerState defaultState = null;
        private PlayerState currentState = null;

        public bool bOnDetectFloor;
        public bool bOnFloor;
        public bool bOnCeil;
        public bool bOnWallFrontT;
        public bool bOnWallFrontB;
        public bool bOnWallFront;
        public bool bOnWallBackT;
        public bool bOnWallBackB;
        public bool bOnWallBack;
        public bool bOnLedgeHorizontal;
        public bool bOnLedgeVertical;
        public bool bOnLedge;

        public PlayerData data;
        public PlayerIdle idleLong;
        public PlayerIdleShort idleShort;
        public PlayerWalk walk;
        public PlayerRun run;
        public _PlayerSit sit;
        public _PlayerHeadUp headUp;
        public _PlayerFreeFall freeFall;
        public _PlayerGliding gliding;
        public PlayerIdleWallFront idleWallFront;
        public PlayerSlidingWallFront slidingWallFront;
        public _PlayerJumpOnFloor jumpOnFloor;
        public _PlayerJumpOnAir jumpOnAir;
        public _PlayerJumpOnWallFront jumpOnWallFront;
        public PlayerRoll roll;
        public PlayerDash dash;
        public _PlayerClimbOnLedge climbLedge;
        public PlayerAttackOnFloor attackOnFloor;
        public PlayerAttackOnAir attackOnAir;
        public PlayerAbilitySword abilitySword;
        public PlayerAbilityGun abilityGun;
        public PlayerTakeDown takeDown;

        public bool bIsRun = false;
        public int leftAirJumpCount = 0;
        public Vector2 cameraOffset = Vector2.zero;

        private void OnValidate()
        {
            if(Application.isEditor && !Application.isPlaying)
                TryGetComponent<Player>(out m_player);
        }

        public void OnStart()
        {
            if(m_player == null)
                TryGetComponent<Player>(out m_player);

            int idx = -1;

            // data = new PlayerData();

            idleLong = new PlayerIdle(m_player, data, ++idx, "IdleLong");
            idleShort = new PlayerIdleShort(m_player, data, ++idx, "IdleShort");
            walk = new PlayerWalk(m_player, data, ++idx, "Walk");
            run = new PlayerRun(m_player, data, ++idx, "Run");
            sit = new _PlayerSit(m_player, data, ++idx, "Sit");
            headUp = new _PlayerHeadUp(m_player, data, ++idx, "HeadUp");
            freeFall = new _PlayerFreeFall(m_player, data, ++idx, "FreeFall");
            gliding = new _PlayerGliding(m_player, data, ++idx, "Gliding");
            idleWallFront = new PlayerIdleWallFront(m_player, data, ++idx, "IdleWallFront");
            slidingWallFront = new PlayerSlidingWallFront(m_player, data, ++idx, "SlidingWallFront");
            jumpOnFloor = new _PlayerJumpOnFloor(m_player, data, ++idx, "JumpOnFloor");
            jumpOnAir = new _PlayerJumpOnAir(m_player, data, ++idx, "JumpOnAir");
            jumpOnWallFront = new _PlayerJumpOnWallFront(m_player, data, ++idx, "JumpOnWallFront");
            roll = new PlayerRoll(m_player, data, ++idx, "Roll");
            dash = new PlayerDash(m_player, data, ++idx, "Dash");
            climbLedge = new _PlayerClimbOnLedge(m_player, data, ++idx, "ClimeLedge");
            attackOnFloor = new PlayerAttackOnFloor(m_player, data, ++idx, "AttackOnFloor");
            attackOnAir = new PlayerAttackOnAir(m_player, data, ++idx, "AttackOnAir");
            abilitySword = new PlayerAbilitySword(m_player, data, ++idx, "AbilitySword");
            abilityGun = new PlayerAbilityGun(m_player, data, ++idx, "AbilityGun");
            takeDown = new PlayerTakeDown(m_player, data, ++idx, "TakeDown");

            defaultState = idleShort;
        }

        public void OnUpdate()
        {
            currentState.OnUpdate();

            attackOnFloor.UpdateCoyoteTime();

            attackOnFloor.UpdateCooltime();
            attackOnAir.UpdateCooltime();
            abilitySword.UpdateCooltime();
            abilityGun.UpdateCooltime();
        }

        public void OnFixedUpdate()
        {
            ++totalFps;
            ++fps;
            currentState.OnFixedUpdate();
        }

        public void Begin(PlayerState initState)
        {
            if(currentState != null)
                return;

            totalFps = -1;
            fps = -1;
            currentState = initState;
            currentState.OnStateBegin();
            m_player?.ChangeAnimation(currentState);
        }

        public void Change(PlayerState nextState)
        {
            if(currentState == null)
                return;

            currentState.OnStateEnd();
            fps = -1;
            currentState = nextState;
            currentState.OnStateBegin();
            m_player?.ChangeAnimation(currentState);
        }

        public void Replay()
        {
            if(currentState == null)
                return;
            currentState.OnStateEnd();
            fps = -1;
            currentState.OnStateBegin();
            m_player?.ChangeAnimation(currentState);
        }

        public void End()
        {
            if(currentState == null)
                return;

            currentState.OnStateEnd();
            currentState = null;
            m_player?.ChangeAnimation(defaultState);
        }

        // 선딜레이 시작 지점
        public void TriggerBeginOfAnimation() => currentState.OnAnimationBegin();

        // 로직 시작 지점
        public void TriggerBeginOfAction() => currentState.OnActionBegin();

        // 후딜레이 시작 지점
        public void TriggerEndOfAction() => currentState.OnActionEnd();

        // 애니메이션 종료 지점
        public void TriggerEndOfAnimation() => currentState.OnAnimationEnd();
    }
}