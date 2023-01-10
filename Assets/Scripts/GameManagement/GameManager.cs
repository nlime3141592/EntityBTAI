using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance => m_instance;
        private static GameManager m_instance;

        public CameraTraceModule camModule;

        public bool bGameStarted { get; private set; }

        private void Start()
        {
            if(m_instance == null)
            {
                m_instance = this;
                StartCoroutine(m_OnProgramStart());
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

        private IEnumerator m_OnProgramStart()
        {
            yield return new WaitForSeconds(0.5f);
            yield return FadeManager.FadeIn(1.2f);
        }

        public void OnGameStart()
        {
            StartCoroutine(m_OnGameStart());
        }

        private IEnumerator m_OnGameStart()
        {
            yield return FadeManager.FadeOut(0.7f);
            yield return MapManager.Open(1);
            Player player = Player.instance;
            player.transform.position =new Vector3(18, 13, 0);
            camModule.Alloc(player.transform);
            yield return new WaitForSeconds(0.5f);
            yield return FadeManager.FadeIn(1.2f);
            bGameStarted = true;
        }

        public void OnGameEnd()
        {
            bGameStarted = false;
        }
    }
}