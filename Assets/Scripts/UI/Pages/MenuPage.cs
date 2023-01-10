using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class MenuPage : MonoBehaviour
    {
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void OnGameStart()
        {
            GameManager.instance.OnGameStart();
            this.gameObject.SetActive(false);
        }
    }
}