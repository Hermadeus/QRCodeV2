using System.Collections;
using System.Collections.Generic;
using QRCode.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackPlaySound : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public SoundName SoundName = SoundName.NULL_Name;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public Transform Target = null;

        //--<Private>
        private AudioController AudioController;
        
        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<
        public FeedbackPlaySound(MonoBehaviour owner, SoundName soundName, Transform target) : base(owner)
        {
            SoundName = soundName;
            Target = target;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            Audio.Audio.PlaySound(SoundName, Target, out AudioController);
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();
            AudioController?.Stop();
            return this;
        }
    }
}
