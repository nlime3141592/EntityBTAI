using System;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : EntityBehaviour
    {
        // TODO: Debug가 완료되면 set;을 private set;으로 변경.
        public bool bBeginOfAnimation { get; private set; }
        public bool bBeginOfAction { get; private set; }
        public bool bEndOfAction { get; private set; }
        public bool bEndOfAnimation { get; private set; }

        public event Action onBeginOfAnimation;
        public event Action onBeginOfAction;
        public event Action onEndOfAction;
        public event Action onEndOfAnimation;

        private Animator m_animator;

        protected override void OnValidate()
        {
            base.OnValidate();

            TryGetComponent<Animator>(out m_animator);
        }

        protected override void Awake()
        {
            base.Awake();

            TryGetComponent<Animator>(out m_animator);
        }

        public void SetState(int _id)
        {
            m_animator.SetInteger("state", _id);
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