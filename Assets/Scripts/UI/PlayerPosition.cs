using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class PlayerPosition : MonoBehaviour
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
                txt.text = string.Format("position: ({0}, {1})", p.transform.position.x, p.transform.position.y);
            }
        }
    }
}