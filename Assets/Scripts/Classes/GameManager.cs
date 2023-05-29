using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public sealed class GameManager : Manager<GameManager>
    {
        public GamePage gamePage;
        public MenuPage menuPage;

        public VirtualCameraFollower vCamFollower;

        public int firstMap;
        public Vector2 firstSpawnPoint;

        public bool bGameStarted => m_bGameStarted;
        public bool m_bGameStarted = false; // NOTE: 테스트 중일 때만 public으로 선언, 실제 릴리즈 시에 private으로 변경합니다.

        public LinkedList<EntitySpawnData> generatedBoss;
        public List<Entity> lBoss;

        public GameManager()
        {
            generatedBoss = new LinkedList<EntitySpawnData>();
            lBoss = new List<Entity>();
        }

        public void ListenQuitGame()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            Player.instance.bGameStarted = this.bGameStarted;
        }
/*
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
            m_bGameStarted = true;
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
            m_bGameStarted = false;
        }
*/
    }
}