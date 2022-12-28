using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityPlayerConfig : ConfigurationBT, IEntityPlayerConfig
    {
        #region Entity Config
        int IEntityConfig.currentState { get; set; } = 0;
        #endregion

        #region Entity Movement Config
        IEntityMovementConfig IEntityMovementConfig.movementConfig => m_movementConfig;
        private IEntityMovementConfig m_movementConfig;

        Vector2 IEntityMovementConfig.moveDir { get; set; } = Vector2.zero;
        Rigidbody2D IEntityMovementConfig.physics { get; set; }
        VelocityController2D IEntityMovementConfig.velModule { get; set; }

        float IEntityMovementConfig.baseSpeed { get; set; } = 3.0f;
        float IEntityMovementConfig.gravity { get; set; } = -9.81f;
        #endregion

        #region Entity Look Config
        IEntityLookConfig IEntityLookConfig.lookConfig => m_lookConfig;
        private IEntityLookConfig m_lookConfig;

        bool IEntityLookConfig.bFixLookDirX { get; set; } = false;
        bool IEntityLookConfig.bFixLookDirY { get; set; } = true;

        int IEntityLookConfig.lookDirX { get; set; } = 1;
        int IEntityLookConfig.lookDirY { get; set; } = -1;
        #endregion

        #region Entity Input Config
        IEntityInputConfig IEntityInputConfig.inputConfig => m_inputConfig;
        private IEntityInputConfig m_inputConfig;

        float IEntityInputConfig.xNegative { get; set; } = 0.0f;
        float IEntityInputConfig.xPositive { get; set; } = 0.0f;
        float IEntityInputConfig.yNegative { get; set; } = 0.0f;
        float IEntityInputConfig.yPositive { get; set; } = 0.0f;

        float IEntityInputConfig.xInput
        {
            get
            {
                IEntityInputConfig config = m_inputConfig;
                return config.xPositive - config.xNegative;
            }
        }

        float IEntityInputConfig.yInput
        {
            get
            {
                IEntityInputConfig config = m_inputConfig;
                return config.yPositive - config.yNegative;
            }
        }
        #endregion

        #region Entity Run Config
        bool IEntityRunConfig.isRun { get; set; } = true;
        #endregion

        #region Entity Player Config
        IEntityPlayerConfig IEntityPlayerConfig.playerConfig => m_playerConfig;
        private IEntityPlayerConfig m_playerConfig;

        ITerrainCheckerConfig IEntityPlayerConfig.floorConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.ceilConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.lbWallConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.rbWallConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.ltWallConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.rtWallConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.lbLedgeConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.rbLedgeConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.ltLedgeConfig { get; set; }
        ITerrainCheckerConfig IEntityPlayerConfig.rtLedgeConfig { get; set; }

        int IEntityPlayerConfig.maxIdleFrame { get; set; } = 200;
        #endregion

        public EntityPlayerConfig()
        {
            m_movementConfig = (IEntityMovementConfig)this;
            m_lookConfig = (IEntityLookConfig)this;
            m_inputConfig = (IEntityInputConfig)this;
            m_playerConfig = (IEntityPlayerConfig)this;

            m_playerConfig.floorConfig = new TerrainCheckerConfig();
            m_playerConfig.ceilConfig = new TerrainCheckerConfig();
            m_playerConfig.lbWallConfig = new TerrainCheckerConfig();
            m_playerConfig.rbWallConfig = new TerrainCheckerConfig();
            m_playerConfig.ltWallConfig = new TerrainCheckerConfig();
            m_playerConfig.rtWallConfig = new TerrainCheckerConfig();
            m_playerConfig.lbLedgeConfig = new TerrainCheckerConfig();
            m_playerConfig.rbLedgeConfig = new TerrainCheckerConfig();
            m_playerConfig.ltLedgeConfig = new TerrainCheckerConfig();
            m_playerConfig.rtLedgeConfig = new TerrainCheckerConfig();
        }

        void IEntityLookConfig.UpdateLookDir()
        {
            m_lookConfig.lookDirX = m_UpdateLookDir(m_lookConfig.bFixLookDirX, m_lookConfig.lookDirX, 1, m_inputConfig.xInput);
            m_lookConfig.lookDirY = m_UpdateLookDir(m_lookConfig.bFixLookDirY, m_lookConfig.lookDirY, -1, m_inputConfig.yInput);
        }

        private int m_UpdateLookDir(bool bFix, int curLookDir, int defaultLookDir, float input)
        {
            if(defaultLookDir != -1 && defaultLookDir != 1)
                defaultLookDir = 1;
            if(curLookDir != -1 && curLookDir != 1)
                curLookDir = defaultLookDir;
            
            if(bFix)
                return curLookDir;
            else if(input < 0.0f)
                return -1;
            else if(input > 0.0f)
                return 1;
            else
                return curLookDir;
        }
    }
}