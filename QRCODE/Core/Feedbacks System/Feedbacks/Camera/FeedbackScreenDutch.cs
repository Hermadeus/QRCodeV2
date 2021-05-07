using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackScreenDutch : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public CinemachineVirtualCamera camera;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public InOutFloat inOut;

        //--<Private>
        private readonly float initDutch;
        private Tween _dutchTween;
        private Coroutine _dutchCor;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackScreenDutch() : base()
        {
            inOut = new InOutFloat(180);
            initDutch = 0f;
        }

        public FeedbackScreenDutch(MonoBehaviour owner, CinemachineVirtualCamera camera) : base(owner)
        {
            this.camera = camera;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            _dutchCor = Owner.StartCoroutine(DutchCor());
            yield break;
        }

        private IEnumerator DutchCor()
        {
            _dutchTween = camera.DoDutch(inOut.amplitude, inOut.inDuration, inOut.inEase);
            yield return new WaitForSeconds(inOut.betweenDuration + inOut.inDuration);
            _dutchTween = camera.DoDutch(initDutch, inOut.outDuration, inOut.outEase);
            _dutchTween = camera.DoDutch(initDutch, inOut.outDuration, inOut.outEase);
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(_dutchCor != null) Owner.StopCoroutine(_dutchCor);
            _dutchTween?.Kill();
            return this;
        }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public FeedbackScreenDutch SetAmplitude(float value)
        {
            inOut.amplitude = value;
            return this;
        }
        
        public FeedbackScreenDutch SetCamera(CinemachineVirtualCamera value)
        {
            camera = value;
            return this;
        }
    }
}
