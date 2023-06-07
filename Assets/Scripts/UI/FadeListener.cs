using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class FadeListener : MonoBehaviour
    {
        private Image img;

        private void Awake()
        {
            img = GetComponent<Image>();
        }

        private void Update()
        {
            Color clr = img.color;
            img.color = new Color(clr.r, clr.g, clr.b, Loading.fader.value);
        }
    }
}