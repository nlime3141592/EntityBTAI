using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnchordMetroidvania
{
    public class PlayerVelocity : MonoBehaviour
    {
        private Text txt;

        private void Start()
        {
            txt = GetComponent<Text>();
        }

        private void Update()
        {
            Player p = Player.instance;

            if(p == null)
                txt.text = "(-, -)";
            else
            {
                txt.text = string.Format("velocity: ({0}, {1})", p.vm.x, p.vm.y);
            }
        }
    }
}