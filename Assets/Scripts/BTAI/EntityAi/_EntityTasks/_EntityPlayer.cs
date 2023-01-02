using UnityEngine;

namespace UnchordMetroidvania
{
    public class _EntityPlayer : _EntityBase
    {
        public LTRB ltrb;
        public TestBoxSkill bxSkill;

        public BattleModule btModule;

        public bool applyBonusStrength;
        private bool m_applyBonusStrengh;

        private StatModifier strMod;

        protected override void Start()
        {
            base.Start();

            bxSkill = new TestBoxSkill(
                "TestBoxSkill", 315789474, 0,
                10,
                TargetSortType.None, false,
                ltrb);
            bxSkill.bRangeOnEditor = true;
            btModule = GetComponent<BattleModule>();
            strMod = new StatModifier(0.4f, StatModType.PercentMul, this);

            Debug.Log(string.Format("[Skill Information]\n  ID: {0}\n  Name: {1}\n  LTRB: ({2}/{3}/{4}/{5})", 
            bxSkill.id, bxSkill.name, bxSkill.range.left, bxSkill.range.top, bxSkill.range.right, bxSkill.range.bottom));
        }

        protected override void Update()
        {
            base.axisInput.x = Input.GetAxisRaw("Horizontal");
            base.axisInput.y = Input.GetAxisRaw("Vertical");

            if(Input.GetKeyDown(KeyCode.Space))
            {
                btModule.UseBattleSkill(bxSkill);
            }

            if(applyBonusStrength ^ m_applyBonusStrengh)
            {
                if(applyBonusStrength)
                    strength.AddModifier(strMod);
                else
                    strength.RemoveModifier(strMod);
            }

            m_applyBonusStrengh = applyBonusStrength;
        }
    }
}