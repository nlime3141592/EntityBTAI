using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class PlayerHealthUI : MonoBehaviour
    {
        private Player player;
        public Text txt;

        private void Start()
        {
            player = Player.instance;
        }

        private void Update()
        {
            txt.text = string.Format("{0}/{1}", player.health, player.maxHealth.finalValue);
        }
    }
}