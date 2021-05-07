using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

namespace QRCode.Feedbacks
{
    [Serializable]
    public class FeedbackImageFader : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]   
        public Image Image = default;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]   
        public float To  = 0f;
        
        //--<Private>
        private Tween fadeTween;

        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<
        public FeedbackImageFader() : base()
        {
            To = 0;
        }
        
        public FeedbackImageFader(MonoBehaviour owner, Image image) : base(owner)
        {
            Image = image;
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            fadeTween = Image.DOFade(To, Duration).SetEase(Ease);
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();
            fadeTween.Kill();
            return this;
        }
    }
}
