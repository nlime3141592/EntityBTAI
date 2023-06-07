// NOTE: Game Manager의 역할을 수행합니다.

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public class PlayerScene : SceneComponent
    {
        // public UnchordGuiPage gamePage;
        // public UnchordGuiPage menuPage;

        public static PlayerScene instance { get; private set; }

        [Header("Camera Settings")]
        public VirtualCameraFollower vCamFollower;

        [Header("Hidden Property Settings")]
        public Vector2 hiddenPosition;

        private void Awake()
        {
            if(instance == null)
                instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
                m_TestLoading();
        }

        public bool FixToHiddenFloor()
        {
            float px = hiddenPosition.x;
            float py = hiddenPosition.y;
            float pz = Player.instance.transform.position.z;
            Player.instance.transform.position = new Vector3(px, py, pz);
            return true;
        }

        private void m_TestLoading()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync("LoadingScene01", LoadSceneMode.Additive);
            Loading.StartLoading();

            Loading.cmdQueue.Enqueue(Loading.fader.GetFadeOutCommand(1.5f));
            Loading.cmdQueue.Enqueue(Loading.ClearDelay);
            Loading.cmdQueue.Enqueue(Loading.GetUpdateDelayCommand(2.5f));
            Loading.cmdQueue.Enqueue(Loading.fader.GetFadeInCommand(0.75f));
            Loading.cmdQueue.Enqueue(() => Map.TryCloseMap("LoadingScene01"));
            Loading.cmdQueue.Enqueue(Loading.EndLoading);
        }
    }
}