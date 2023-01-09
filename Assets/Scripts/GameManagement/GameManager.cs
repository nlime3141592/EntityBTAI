using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance => m_instance;
        private static GameManager m_instance;

        public bool bGameStarted { get; private set; }

        private void Start()
        {
            if(m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void Update()
        {
            
        }

        public void OnGameStart()
        {
            bGameStarted = true;
        }

        public void OnGameEnd()
        {
            bGameStarted = false;
        }
    }
}