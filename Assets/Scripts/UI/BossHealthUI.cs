using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class BossHealthUI : MonoBehaviour
    {
        public Text txt;
        private EntitySpawnData m_spawnData;

        private void Start()
        {

        }

        public void SetBoss(EntitySpawnData spawnData)
        {
            m_spawnData = spawnData;
        }

        private void Update()
        {
            string name = m_spawnData.name;
            float health = m_spawnData.entity.health;
            float maxHealth = m_spawnData.entity.maxHealth.finalValue;

            txt.text = string.Format("{0}: {1}/{2}", name, health, maxHealth);
        }

        private void OnDisable()
        {
            txt.text = "";
        }
    }
}