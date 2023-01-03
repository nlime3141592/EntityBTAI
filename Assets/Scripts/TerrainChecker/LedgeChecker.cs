using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public sealed class LedgeChecker : TerrainChecker
    {
        private PlainChecker m_wall;
        private PlainChecker m_floor;

        public static LedgeChecker Get(
            ConfigurationBT<TerrainCheckResult> config, string name,
            PlainChecker wallChecker, PlainChecker floorChecker)
        {
            LedgeChecker checker = new LedgeChecker(config, 4, name, wallChecker, floorChecker);
            return checker;
        }

        private LedgeChecker(
            ConfigurationBT<TerrainCheckResult> config, int id, string name,
            PlainChecker wallChecker, PlainChecker floorChecker)
        : base(config, id, name)
        {
            m_wall = wallChecker;
            m_floor = floorChecker;
        }

        protected override InvokeResult p_Invoke()
        {
            m_wall.Invoke();

            if(!m_wall.config.instance.bDetected)
            {
                m_floor.Invoke();

                config.instance.bDetected = m_floor.config.instance.bDetected;
                config.instance.bHit = m_floor.config.instance.bHit;
                config.instance.terrain = m_floor.config.instance.terrain;
            }
            else
            {
                config.instance.bDetected = false;
                config.instance.bHit = false;
                config.instance.terrain = default(RaycastHit2D);
            }

            return InvokeResult.Success;
        }
    }
}