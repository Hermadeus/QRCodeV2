using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackOrthographicSize : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public CinemachineVirtualCamera camera = default;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public InOutFloat inOut;

         //--<Privates>
        private float _initOrthoSize;
        private Coroutine _orthoSizeCor;
        private Tween _orthoTween;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackOrthographicSize() : base()
        {
            inOut = new InOutFloat(5.5f);
        }

        public FeedbackOrthographicSize(MonoBehaviour owner, CinemachineVirtualCamera camera) : base(owner)
        {
            this.camera = camera;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            _initOrthoSize = camera.m_Lens.OrthographicSize;
            _orthoSizeCor = Owner.StartCoroutine(OrthographicCor());
            yield break;
        }

        IEnumerator OrthographicCor()
        {
            _orthoTween = camera.DoOrthographicSize(inOut.amplitude, inOut.inDuration, inOut.inEase);
            yield return new WaitForSeconds(inOut.betweenDuration);
            _orthoTween = camera.DoOrthographicSize(_initOrthoSize, inOut.outDuration, inOut.outEase);
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(_orthoSizeCor != null) Owner.StopCoroutine(_orthoSizeCor);
            _orthoTween?.Kill();
            return this;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public FeedbackOrthographicSize SetCamera(CinemachineVirtualCamera value)
        {
            camera = value;
            return this;
        }
        
        public FeedbackOrthographicSize SetAmplitude(float value)
        {
            inOut.amplitude = value;
            return this;
        }
    }
}

