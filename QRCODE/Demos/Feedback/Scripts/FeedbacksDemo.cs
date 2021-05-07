using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks.Demos
{
    public class FeedbacksDemo : SerializedMonoBehaviour
    {
        private Feedbacks Feedbacks = new Feedbacks();

        private void Start()
        {
            Feedbacks.Init(gameObject);
        }

        [Button]
        private void CallFeedback(string key)
        {
            Feedbacks.Call(key);
        }
    }
}
