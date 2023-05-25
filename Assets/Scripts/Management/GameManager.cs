using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance => m_instance;
        private static GameManager m_instance;

        public GamePage gamePage;
        public MenuPage menuPage;

        public VirtualCameraFollower vCamFollower;

        public int firstMap;
        public Vector2 firstSpawnPoint;

        public bool bGameStarted { get; private set; } = false;

        public LinkedList<EntitySpawnData> generatedBoss;
        public List<Entity> lBoss;

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

            Player.instance.bGameStarted = this.bGameStarted;
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
            yield return StartCoroutine(MapManager.Open(firstMap, m_OnOpenScene));
            yield return FadeManager.FadeIn(1.2f);
            bGameStarted = true;
        }

        private void m_OnOpenScene()
        {
            Player player = Player.instance;
            player.transform.position = firstSpawnPoint;
            vCamFollower.Follow(player.transform, Vector2.up, 0);
            gamePage.gameObject.SetActive(true);
        }

        public void OnGameEnd()
        {
            bGameStarted = false;
        }
    }
}