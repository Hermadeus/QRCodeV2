using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace QRCode.Feedbacks
{
    public class FeedbackScreenFlash : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public Image image = default;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public InOutFloat inOut;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool changeColorAlongFlash = false;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>"), ShowIf("@this.changeColorAlongFlash")]
        public Gradient gradient = default;

        //--<Private>
        private Coroutine 
            _flashCoroutine,
            _thisupdateCoroutine;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackScreenFlash() : base()
        {
            inOut = new InOutFloat(1f);
            gradient = new Gradient();
            changeColorAlongFlash = false;
        }

        public FeedbackScreenFlash(MonoBehaviour owner, Image image) : base(owner)
        {
            this.image = image;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            image.fillAmount = 0;
            if(changeColorAlongFlash) _thisupdateCoroutine = Owner.StartCoroutine(ChangeColorAlongTime());
            _flashCoroutine = Owner.StartCoroutine(FlashCor());
            yield break;
        }

        IEnumerator FlashCor()
        {
            image.DOFade(inOut.amplitude, inOut.inDuration).SetEase(inOut.inEase);
            yield return new WaitForSeconds(inOut.betweenDuration);
            image.DOFade(0f, inOut.outDuration).SetEase(inOut.outEase);
        }
        
        IEnumerator ChangeColorAlongTime()
        {
            var totalTime = inOut.inDuration + inOut.outDuration + inOut.betweenDuration;
            var time = 0f;
            while (time < totalTime)
            {
                time += FeedbackMaker.FeedbackDeltaTime;
                var _color = gradient.Evaluate(1 / (totalTime / time));
                image.color = new Color(_color.r, _color.g, _color.b, image.color.a);

                yield return null;
            }
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(changeColorCoroutine != null) Owner.StopCoroutine(_thisupdateCoroutine);
            if(_flashCoroutine != null) Owner.StopCoroutine(_flashCoroutine);
            return this;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public FeedbackScreenFlash SetImage(Image value)
        {
            image = value;
            return this;
        }
        
        public FeedbackScreenFlash SetAmplitude(float value)
        {
            inOut.amplitude = value;
            return this;
        }
        
        public FeedbackScreenFlash SetGradient(Gradient value = null)
        {
            if (value == null)
            {
                changeColorAlongFlash = false;
                return this;
            }
            
            changeColorAlongFlash = true;
            gradient = value;
            return this;
        }
    }
}
