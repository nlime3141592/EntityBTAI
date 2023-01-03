using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityPlayer : EntityBase
    {
        public LTRB ltrb;
        public TestBoxSkill btSkill;

        public BattleModule btModule;

        public TEST_STAT_BONUS_TABLE bonusStrength;
        public TEST_STAT_BONUS_TABLE bonusDefence;

        protected override void Start()
        {
            base.Start();

            btSkill = new TestBoxSkill(
                "TestBoxSkill", 315789474, 0,
                10,
                TargetSortType.None, false,
                ltrb
            );
            btSkill.bRangeOnEditor = true;
            btModule = GetComponent<BattleModule>();

            bonusStrength.Start(strength);
            bonusDefence.Start(defence);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();

            base.axisInput.x = Input.GetAxisRaw("Horizontal");
            base.axisInput.y = Input.GetAxisRaw("Vertical");

            if(Input.GetKeyDown(KeyCode.Space))
                btModule.UseBattleSkill(btSkill);
        }
    }

    [Serializable]
    public class TEST_STAT_BONUS_TABLE
    {
        [Tooltip("테스트는 0~100까지 적용 가능합니다.")]
        [Range(0, 100)] public float flatValue = 0;

        [Tooltip("테스트는 0%p~200%p까지 적용 가능합니다.")]
        [Range(0, 2)] public float percentPoint = 0;

        [Tooltip("테스트는 0%~200%까지 적용 가능합니다.")]
        [Range(0, 2)] public float percent = 0;

        private StatModifier modFlatValue;
        private StatModifier modPercentPoint;
        private StatModifier modPercent;

        public void Start(Stat stat)
        {
            modFlatValue = new StatModifier(flatValue, StatModType.Flat);
            modPercentPoint = new StatModifier(percentPoint, StatModType.PercentAdd);
            modPercent = new StatModifier(percent, StatModType.PercentMul);

            stat.AddModifier(modFlatValue);
            stat.AddModifier(modPercentPoint);
            stat.AddModifier(modPercent);
        }
    }
}