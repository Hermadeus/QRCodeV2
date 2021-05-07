using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackGenericHandler<T> : SerializedMonoBehaviour, IFeedbackHandler where T : Feedback
    {
        [SerializeField] private bool playOnStart = false;
        public T Feedback;

        private void Start()
        {
            if (Feedback is Sequencer)
            {
                var sequencer = Feedback as Sequencer;
                sequencer.SetOwnerForAll(this);
            }
            else
            {
                Feedback.SetOwner(this);
            }
            if (playOnStart) Feedback.Play();
        }

        public virtual void Play()
        {
            Feedback.Play();
        }
    }

    public interface IFeedbackHandler
    {
        void Play();
    }
}
