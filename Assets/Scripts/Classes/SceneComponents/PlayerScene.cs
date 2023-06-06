// NOTE: Game Manager의 역할을 수행합니다.

using System;
using UnityEngine;

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

        public bool FixToHiddenFloor()
        {
            float px = hiddenPosition.x;
            float py = hiddenPosition.y;
            float pz = Player.instance.transform.position.z;
            Player.instance.transform.position = new Vector3(px, py, pz);
            return true;
        }
    }
}