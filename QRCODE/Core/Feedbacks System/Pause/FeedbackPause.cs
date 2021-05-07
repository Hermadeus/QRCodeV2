using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackPause : Feedback
    {
        public FeedbackPause(): base()
        {
            useDurationAsDelayAfter = true;
        }
        
        public override IEnumerator Describe()
        {
            yield return new WaitForSeconds(Duration);
        }
    }
}