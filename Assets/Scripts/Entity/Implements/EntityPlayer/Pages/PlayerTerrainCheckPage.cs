using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerTerrainCheckPage : PageNodeBT<TerrainCheckResult>
    {
        // Index Arrangement #1
        // Floor (F)
        // Ceil (C)
        // Wall (LT, RT, LB, RB)
        // Ledge Detector 1, Wall (LT, RT, LB, RB)
        // Ledge Detector 2, Floor or Ceil (LT, RT, LB, RB)
        // Ledge Detector 3, Composite of Ledge Detector 1 and 2 (LT, RT, LB, RB)

        // Index Arrangement #2
        // F, H, LT, RT, LB, RB, HL, HR, FL, FR

        public TerrainCheckResult this[EPlayerTerrainCheckResult result]
        => m_results[(int)result];

        #region Terrain Detection Results
        private readonly TerrainCheckResult[] m_results; // Length: 18
        #endregion

        #region Terrain Detection Configs
        private readonly ConfigurationBT<TerrainCheckResult>[] m_configs; // Length: 18
        #endregion

        #region Terrain Checker
        private readonly PlainChecker[] m_pCheckers; // Length: 14
        private readonly LedgeChecker[] m_lCheckers; // Length: 4
        #endregion

        #region Transforms
        private readonly Transform[] m_tOrigins; // Length: 10
        #endregion

        private ParallelNodeBT<TerrainCheckResult> m_root;

        public PlayerTerrainCheckPage(
            Transform f, Transform h,
            Transform lt, Transform rt, Transform lb, Transform rb,
            Transform hl, Transform hr, Transform fl, Transform fr
        )
        : base(null, -1, "")
        {
            m_results = new TerrainCheckResult[18];
            m_configs = new ConfigurationBT<TerrainCheckResult>[18];
            m_tOrigins = new Transform[10];
            m_pCheckers = new PlainChecker[14];
            m_lCheckers = new LedgeChecker[4];

            SetOrigin(0, f);
            SetOrigin(1, h);
            SetOrigin(2, lt);
            SetOrigin(3, rt);
            SetOrigin(4, lb);
            SetOrigin(5, rb);
            SetOrigin(6, hl);
            SetOrigin(7, hr);
            SetOrigin(8, fl);
            SetOrigin(9, fr);

            InitTerrainCheckers();
        }

        private void SetOrigin(int index, Transform origin)
        {
            m_tOrigins[index] = origin;
        }

        private void InitTerrainCheckers()
        {
            for(int j = 0; j < 18; ++j)
            {
                m_results[j] = new TerrainCheckResult();
                m_configs[j] = new ConfigurationBT<TerrainCheckResult>(m_results[j]);
            }

            int i = -1;
            ++i; m_pCheckers[i] = PlainChecker.GetFloor(m_configs[i], "FloorChecker");
            ++i; m_pCheckers[i] = PlainChecker.GetCeil(m_configs[i], "CeilChecker");

            ++i; m_pCheckers[i] = PlainChecker.GetLeftWall(m_configs[i], "WallCheckerLT");
            ++i; m_pCheckers[i] = PlainChecker.GetRightWall(m_configs[i], "WallCheckerRT");
            ++i; m_pCheckers[i] = PlainChecker.GetLeftWall(m_configs[i], "WallCheckerLB");
            ++i; m_pCheckers[i] = PlainChecker.GetRightWall(m_configs[i], "WallCheckerRB");

            ++i; m_pCheckers[i] = PlainChecker.GetLeftWall(m_configs[i], "LedgeCheckerLT1");
            ++i; m_pCheckers[i] = PlainChecker.GetRightWall(m_configs[i], "LedgeCheckerRT1");
            ++i; m_pCheckers[i] = PlainChecker.GetLeftWall(m_configs[i], "LedgeCheckerLB1");
            ++i; m_pCheckers[i] = PlainChecker.GetRightWall(m_configs[i], "LedgeCheckerRB1");

            ++i; m_pCheckers[i] = PlainChecker.GetFloor(m_configs[i], "LedgeCheckerLT2");
            ++i; m_pCheckers[i] = PlainChecker.GetFloor(m_configs[i], "LedgeCheckerRT2");
            ++i; m_pCheckers[i] = PlainChecker.GetCeil(m_configs[i], "LedgeCheckerLB2");
            ++i; m_pCheckers[i] = PlainChecker.GetCeil(m_configs[i], "LedgeCheckerRB2");

            ++i; m_lCheckers[i - 14] = LedgeChecker.Get(m_configs[i], "LedgeCheckerLT3", m_pCheckers[i - 8], m_pCheckers[i - 4]);
            ++i; m_lCheckers[i - 14] = LedgeChecker.Get(m_configs[i], "LedgeCheckerRT3", m_pCheckers[i - 8], m_pCheckers[i - 4]);
            ++i; m_lCheckers[i - 14] = LedgeChecker.Get(m_configs[i], "LedgeCheckerLB3", m_pCheckers[i - 8], m_pCheckers[i - 4]);
            ++i; m_lCheckers[i - 14] = LedgeChecker.Get(m_configs[i], "LedgeCheckerRB3", m_pCheckers[i - 8], m_pCheckers[i - 4]);

            for(int j = 0; j < 10; ++j)
            {
                m_pCheckers[j].tOrigin = m_tOrigins[j];
                m_pCheckers[j].detectLength = 0.5f;
                m_pCheckers[j].hitLength = 0.07f;
            }
            for(int j = 0; j < 4; ++j)
            {
                m_pCheckers[10 + j].detectLength = 0.5f;
                m_pCheckers[10 + j].tOrigin = m_tOrigins[6 + j];
            }

            float dLength = 0.3f;
            m_pCheckers[10].detectLength = dLength;
            m_pCheckers[11].detectLength = dLength;
            m_pCheckers[12].detectLength = dLength;
            m_pCheckers[13].detectLength = dLength;

            m_UpdateValues();

            float offset = 0.1f;
            m_pCheckers[10].globalOffset = Vector3.left * offset;
            m_pCheckers[11].globalOffset = Vector3.right * offset;
            m_pCheckers[12].globalOffset = Vector3.left * offset;
            m_pCheckers[13].globalOffset = Vector3.right * offset;

            m_root = BehaviorTree.Parallel<TerrainCheckResult>(null, -1, "EntityPlayerTerrainChecker", 10);
            /*
            m_root.Alloc(0, m_pCheckers[0]);
            m_root.Alloc(1, m_pCheckers[1]);
            m_root.Alloc(2, m_pCheckers[2]);
            m_root.Alloc(3, m_pCheckers[3]);
            m_root.Alloc(4, m_pCheckers[4]);
            m_root.Alloc(5, m_pCheckers[5]);
            m_root.Alloc(6, m_lCheckers[0]);
            m_root.Alloc(7, m_lCheckers[1]);
            m_root.Alloc(8, m_lCheckers[2]);
            m_root.Alloc(9, m_lCheckers[3]);
            */
        }

        private void m_UpdateValues()
        {
            m_pCheckers[6].detectLength = m_tOrigins[6].position.y - m_tOrigins[2].position.y;
            m_pCheckers[7].detectLength = m_tOrigins[7].position.y - m_tOrigins[3].position.y;
            m_pCheckers[8].detectLength = m_tOrigins[4].position.y - m_tOrigins[8].position.y;
            m_pCheckers[9].detectLength = m_tOrigins[5].position.y - m_tOrigins[9].position.y;
        }

        protected override InvokeResult p_Invoke()
        {
            m_UpdateValues();
            InvokeResult iResult = m_root.Invoke();
            return iResult;
        }
    }
}