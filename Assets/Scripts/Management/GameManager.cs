using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnchordMetroidvania;

namespace Unchord
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance => m_instance;
        private static GameManager m_instance;

        public CameraTraceModule camModule;
        public GamePage gamePage;
        public MenuPage menuPage;

        public int firstMap;
        public Vector2 firstSpawnPoint;

        public bool bGameStarted { get; private set; }

        public LinkedList<EntitySpawnData> generatedBoss;

        private void Start()
        {
            if(m_instance == null)
            {
                m_instance = this;

                generatedBoss = new LinkedList<EntitySpawnData>();

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
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
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
            menuPage.gameObject.SetActive(false);
            yield return FadeManager.FadeOut(0.7f);
            yield return MapManager.Open(firstMap);
            Player player = Player.instance;
            player.transform.position = firstSpawnPoint;
            camModule.Alloc(player.transform);
            yield return new WaitForSeconds(0.5f);
            gamePage.gameObject.SetActive(true);
            yield return FadeManager.FadeIn(1.2f);
            bGameStarted = true;
        }

        public void OnGameEnd()
        {
            bGameStarted = false;
        }
    }
}