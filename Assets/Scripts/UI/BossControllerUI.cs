using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class BossControllerUI : MonoBehaviour
    {
        public Text txt;

        private void Start()
        {
            
        }

        private void Update()
        {
            List<Entity> lBoss = GameManager.instance.lBoss;

            if(lBoss == null || lBoss.Count == 0)
            {
                txt.text = "";
                return;
            }

            string eName = lBoss[0].entityName;
            int phase = lBoss[0].phase;
            float cHealth = lBoss[0].health;
            float mHealth = lBoss[0].maxHealth.finalValue;
            float cGroggy = lBoss[0].groggyValue;
            float mGroggy = lBoss[0].maxGroggyValue.finalValue;

            txt.text = string.Format($"{eName} (phase-{phase + 1})\nHP: {cHealth}/{mHealth}\nGroggy: {cGroggy}/{mGroggy}");
        }
    }
}