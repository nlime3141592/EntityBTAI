using System;
using UnityEngine;

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
            Loading.fader.speedFadeOut = this.speedFadeOut;
            Loading.fader.speedFadeIn = this.speedFadeIn;

            Loading.cmdQueue.Enqueue(Loading.fader.TryFadeOut);
            Loading.cmdQueue.Enqueue(PlayerScene.instance.vCamFollower.Unfollow);
            Loading.cmdQueue.Enqueue(Map.Close(this.gameObject.scene.name));
            Loading.cmdQueue.Enqueue(PlayerScene.instance.OnMapClose);
            Loading.cmdQueue.Enqueue(Map.Open(nextMap));
            Loading.cmdQueue.Enqueue((_callbackOnEnd) =>
            {
                Player.instance.transform.position = new Vector3(nextPosition.x, nextPosition.y, Player.instance.transform.position.z);
                PlayerScene.instance.vCamFollower.Follow(Player.instance.transform, Vector2.zero, 0);
                _callbackOnEnd();
            });
            Loading.cmdQueue.Enqueue(Loading.GetDelay(0.8f));
            Loading.cmdQueue.Enqueue(Loading.fader.TryFadeIn);

            Loading.StartLoading("LoadingScene01");
        }
    }
}