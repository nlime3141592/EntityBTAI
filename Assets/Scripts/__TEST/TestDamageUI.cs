using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnchordMetroidvania
{    
    public class TestDamageUI : MonoBehaviour
    {
        private Text txt;
        private System.Random prng;

        public float sx = 130f;
        public float sy = 240f;
        public float gravityA = -400f;
        public float gravityB = -1200f;
        public float value;
        public float aSpeed = 0.8f;
        
        private float sa = 1;
        private float vx;
        private float vy;

        public static TestDamageUI Get(TestDamageUI prefab, float value, Vector2 position)
        {
            TestDamageUI instance = GameObject.Instantiate(prefab.gameObject).GetComponent<TestDamageUI>();
            instance.value = value;
            instance.Show();

            RectTransform rt = instance.GetComponent<RectTransform>();
            Vector2 vPos = Camera.main.WorldToViewportPoint(position);
            Vector2 wPos = new Vector2(vPos.x * rt.sizeDelta.x - rt.sizeDelta.x * 0.5f, vPos.y * rt.sizeDelta.y - rt.sizeDelta.y * 0.5f);
            rt.anchoredPosition = wPos;
            return instance;
        }

        private void Start()
        {
            txt = GetComponent<Text>();
            prng = new System.Random();

            float wx = (float)prng.NextDouble() * 2 - 1;
            vx = sx * wx;
            vy = sy;
        }

        private void FixedUpdate()
        {
            if(txt.color.a == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                if(vy > 0)
                    vy += (Time.fixedDeltaTime * gravityA);
                else
                    vy += (Time.fixedDeltaTime * gravityB);

                transform.position += new Vector3(vx * Time.fixedDeltaTime, vy * Time.fixedDeltaTime);
            }
        }

        private void Update()
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, sa);
            sa -= Time.deltaTime * aSpeed;

            if(sa < 0)
                sa = 0;
        }

        public void Show()
        {
            txt.text = string.Format("{0}", Math.Round(value, 4));
        }
    }
}