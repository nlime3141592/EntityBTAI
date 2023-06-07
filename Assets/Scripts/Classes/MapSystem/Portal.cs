using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public class Portal : MonoBehaviour
    {
        public float speedFadeOut = 3.5f;
        public float speedFadeIn = 3.5f;

        public string nextMap = "ErrorMap";
        public Vector2 nextPosition;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.J))
                m_OnPortal();
        }

        private void m_OnPortal()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync("LoadingScene01", LoadSceneMode.Additive);
            Loading.StartLoading();

            Loading.cmdQueue.Enqueue(Loading.fader.GetFadeOutCommand(speedFadeOut));
            Loading.cmdQueue.Enqueue(() =>
            {
                PlayerScene.instance.vCamFollower.Unfollow();
                return true;
            });
            Loading.cmdQueue.Enqueue(PlayerScene.instance.FixToHiddenFloor);
            Loading.cmdQueue.Enqueue(() => Map.TryCloseMap(this.gameObject.scene.name));
            Loading.cmdQueue.Enqueue(() => Map.TryOpenMap(nextMap));
            Loading.cmdQueue.Enqueue(() =>
            {
                Player.instance.transform.position = new Vector3(nextPosition.x, nextPosition.y, Player.instance.transform.position.z);
                PlayerScene.instance.vCamFollower.Follow(Player.instance.transform, Vector2.zero, 0);
                return true;
            });
            Loading.cmdQueue.Enqueue(Loading.fader.GetFadeInCommand(speedFadeIn));
            Loading.cmdQueue.Enqueue(() => Map.TryCloseMap("LoadingScene01"));
            Loading.cmdQueue.Enqueue(Loading.EndLoading);
        }
    }
}