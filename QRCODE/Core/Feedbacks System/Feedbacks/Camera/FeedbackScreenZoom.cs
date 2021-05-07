using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackScreenZoom : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public CinemachineVirtualCamera camera;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public InOutFloat inOut;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool incrementalZoom;

        //--<Private>
        private float _initialZoom;
        private Tween _tweenZoom;
        private Coroutine _zoomCor;

        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackScreenZoom() : base()
        {
            inOut = new InOutFloat(1.2f);
            incrementalZoom = false;
        }
        
        public FeedbackScreenZoom(
            MonoBehaviour owner, CinemachineVirtualCamera camera, 
            float amplitude = 1.2f, float duration = 1f) 
            : base(owner)
        {
            this.camera = camera;
            inOut.amplitude = amplitude;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            _zoomCor = Owner.StartCoroutine(ZoomCor());   
            yield break;
        }

        IEnumerator ZoomCor()
        {
            _initialZoom = camera.m_Lens.FieldOfView;
            var initial = incrementalZoom ? camera.m_Lens.FieldOfView : _initialZoom;
            _tweenZoom = camera.DoFOV(initial / inOut.amplitude, inOut.inDuration, inOut.inEase);
            yield return new WaitForSeconds(inOut.inDuration + inOut.betweenDuration);
            _tweenZoom = camera.DoFOV(initial, inOut.outDuration, inOut.outEase);
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(_zoomCor != null) Owner.StopCoroutine(ZoomCor());
            _tweenZoom?.Kill();
            Debug.Log("aaaa");
            camera.m_Lens.FieldOfView = _initialZoom;
            return this;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public FeedbackScreenZoom SetAmplitude(float value)
        {
            inOut.amplitude = value;
            return this;
        }
        
        public FeedbackScreenZoom SetCamera(CinemachineVirtualCamera value)
        {
            camera = value;
            return this;
        }
    }
}