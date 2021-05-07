using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.Feedbacks
{
    
    public class FeedbackRigidbody : Feedback
    {
        public Rigidbody Rigidbody = default;
        public Vector3 Direction;
        public float Strenght = 1f;

        public FeedbackRigidbody() : base()
        {
            Direction = new Vector3();
            Strenght = 1f;
        }

        public FeedbackRigidbody(MonoBehaviour owner, Rigidbody rigidbody) : base()
        {
            Rigidbody = rigidbody;
        }
        
        public override IEnumerator Describe()
        {
            Rigidbody.AddForce(Direction * (Strenght * FeedbackMaker.FeedbackDeltaTime));
            yield break;
        }
    }
}
