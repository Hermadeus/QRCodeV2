using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackAnimation : Feedback
    {
        public Animator Animator = default;

        public AnimatorKeyType KeyType = AnimatorKeyType.Trigger;
        public string PropertyName = "";

        public float FloatTo = 1f;
        public int IntTo = 1;
        public bool BoolTo = true;

        public FeedbackAnimation(): base()
        {
            FloatTo = 1;
            IntTo = 1;
            BoolTo = true;
        }

        public FeedbackAnimation(MonoBehaviour owner, Animator animator) : base(owner)
        {
            Animator = animator;
        }
        
        public override IEnumerator Describe()
        {
            switch (KeyType)
            {
                case AnimatorKeyType.Float:
                    Animator.SetFloat(PropertyName, FloatTo);
                    break;
                case AnimatorKeyType.Int:
                    Animator.SetInteger(PropertyName, IntTo);
                    break;
                case AnimatorKeyType.Bool:
                    Animator.SetBool(PropertyName, BoolTo);
                    break;
                case AnimatorKeyType.Trigger:
                    Animator.SetTrigger(PropertyName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            yield break;
        }
    }

    public enum AnimatorKeyType
    {
        Float,
        Int,
        Bool,
        Trigger,
    }
}
