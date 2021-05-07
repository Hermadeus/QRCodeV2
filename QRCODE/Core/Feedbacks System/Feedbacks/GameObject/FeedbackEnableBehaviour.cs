using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackEnableBehaviour : Feedback
    {
        public MonoBehaviour ComponentToEnable = default;
        public bool Enable = false;

        public bool Resetable = false;
        
        public FeedbackEnableBehaviour() : base()
        {
            Enable = false;
            Resetable = false;
        }

        public FeedbackEnableBehaviour(MonoBehaviour owner, MonoBehaviour componentToEnable, bool enable = false) : base(owner)
        {
            ComponentToEnable = componentToEnable;
            Enable = enable;
        }
        
        public override IEnumerator Describe()
        {
            ComponentToEnable.enabled = Resetable ? !ComponentToEnable.enabled : Enable;
            yield break;
        }
    }
}

