using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class GamePage : MonoBehaviour
    {
        public PlayerHealthUI playerHealth;
        public BossHealthUI bossHealth;

        void Start()
        {
            
        }

        void Update()
        {
            if(GameManager.instance.generatedBoss.Count > 0)
            {
                bossHealth.gameObject.SetActive(true);
                EntitySpawnData data = GameManager.instance.generatedBoss.First.Value;
                bossHealth.SetBoss(data);
            }
            else
            {
                bossHealth.gameObject.SetActive(false);
            }
        }
    }
}