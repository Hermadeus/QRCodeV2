using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackFreezeFrame : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float LastTimeScale;

        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackFreezeFrame() : base()
        {
            Duration = .5f;
            LastTimeScale = 1f;
        }

        public FeedbackFreezeFrame(MonoBehaviour owner, float duration = .5f) : base(owner)
        {
            Duration = duration;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            yield return new WaitWhile(() => Time.timeScale == 0);

            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(Duration);
            Time.timeScale = 1f;
        }

        public override Feedback Kill()
        {
            base.Kill();
            Time.timeScale = LastTimeScale;
            return this;
        }
    }
}