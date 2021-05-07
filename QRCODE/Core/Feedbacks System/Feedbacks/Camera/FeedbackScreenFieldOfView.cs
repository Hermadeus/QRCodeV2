using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackScreenFieldOfView : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public CinemachineVirtualCamera camera = default;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public InOutFloat inOut;

        //--<Private>
        private float _initFov = 60f;
        private Coroutine _fovCor;
        private Tween _fovTween;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackScreenFieldOfView() : base()
        {
            inOut = new InOutFloat(90f);
        }

        public FeedbackScreenFieldOfView(MonoBehaviour owner, CinemachineVirtualCamera camera, float amplitude = 90f) 
            : base(owner)
        {
            inOut = new InOutFloat(90f);
            this.camera = camera;
            inOut.amplitude = amplitude;
            _initFov = camera.m_Lens.FieldOfView;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            _fovCor = Owner.StartCoroutine(FovCor());
            yield break;
        }

        IEnumerator FovCor()
        {
            _initFov = camera.m_Lens.FieldOfView;
            _fovTween = camera.DoFOV(inOut.amplitude, inOut.inDuration, inOut.inEase);
            yield return new WaitForSeconds(inOut.betweenDuration + inOut.inDuration);
            _fovTween = camera.DoFOV(_initFov, inOut.outDuration, inOut.outEase);
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(_fovCor != null) Owner.StopCoroutine(_fovCor);
            _fovTween?.Kill();
            camera.m_Lens.FieldOfView = _initFov;
            return this;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public FeedbackScreenFieldOfView SetCamera(CinemachineVirtualCamera value)
        {
            camera = value;
            return this;
        }
        
        public FeedbackScreenFieldOfView SetAmplitude(float value)
        {
            inOut.amplitude = value;
            return this;
        }
    }
}
