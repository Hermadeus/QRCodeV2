using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackAudioSource : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]  
        public AudioSource AudioSource;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackAudioSource(MonoBehaviour owner, AudioSource source)
        {
            Owner = owner;
            AudioSource = source;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            AudioSource.Play();
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();
            AudioSource.Play();
            return this;
        }
    }
}
