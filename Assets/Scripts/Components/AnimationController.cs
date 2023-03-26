using System;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        // TODO: Debug가 완료되면 set;을 private set;으로 변경.
        public bool bBeginOfAnimation; // { get; set; }
        public bool bBeginOfAction; // { get; set; }
        public bool bEndOfAction; // { get; set; }
        public bool bEndOfAnimation; // { get; set; }

        public event Action onBeginOfAnimation;
        public event Action onBeginOfAction;
        public event Action onEndOfAction;
        public event Action onEndOfAnimation;

        public int id { get; private set; }
        public int phase { get; private set; }

        private Animator m_animator;

        private void OnValidate()
        {
            if(Application.isEditor && !Application.isPlaying)
                TryGetComponent<Animator>(out m_animator);
        }

        public void Awake()
        {
            if(m_animator == null)
                TryGetComponent<Animator>(out m_animator);
        }

        public void SetState(int id)
        {
            this.id = id;
            m_animator.SetInteger("state", id);
        }

        public void Reset()
        {
            bBeginOfAnimation = false;
            bBeginOfAction = false;
            bEndOfAction = false;
            bEndOfAnimation = false;
        }

        public void TriggerBeginOfAnimationLoop()
        {
            bBeginOfAction = false;
            bEndOfAction = false;
            bEndOfAnimation = false;
            TriggerBeginOfAnimation();
        }

        public void TriggerBeginOfAnimation()
        {
            bBeginOfAnimation = true;
            onBeginOfAnimation?.Invoke();
        }

        public void TriggerBeginOfAction()
        {
            bBeginOfAction = true;
            onBeginOfAction?.Invoke();
        }

        public void TriggerEndOfAction()
        {
            bEndOfAction = true;
            onEndOfAction?.Invoke();
        }

        public void TriggerEndOfAnimation()
        {
            bEndOfAnimation = true;
            onEndOfAnimation?.Invoke();
        }
    }
}