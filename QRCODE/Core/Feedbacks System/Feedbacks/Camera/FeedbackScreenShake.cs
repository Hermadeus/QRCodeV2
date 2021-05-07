using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Feedbacks
{
    [Serializable]
    public class FeedbackScreenShake : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]  
        public CinemachineVirtualCamera camera = default;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>"), MinMaxSlider(0f, 50f)]
        public Vector2 intensity = new Vector2(1, 1);

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>"), MinMaxSlider(0f, 50f)]
        public Vector2 frequencyGain = new Vector2(1, 1);

        //--<Private>
        private Coroutine _shakeCoroutine;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackScreenShake() : base()
        {
            Duration = .2f;
            intensity = new Vector2(1f, 1f);
            frequencyGain = new Vector2(1f, 1f);
        }
        
        public FeedbackScreenShake(MonoBehaviour owner, CinemachineVirtualCamera camera) : base(owner)
        {
            Duration = .2f;
            this.camera = camera;
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            var component = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            component.m_AmplitudeGain = Random.Range(intensity.x, intensity.y);
            component.m_FrequencyGain = Random.Range(frequencyGain.x, frequencyGain.y);
            if(_shakeCoroutine != null) Owner.StopCoroutine(_shakeCoroutine);
            _shakeCoroutine = Owner.StartCoroutine(Shake());
            yield break;
        }

        private IEnumerator Shake()
        {
            var timer = Duration;
            while (timer > 0)
            {
                timer -= FeedbackMaker.FeedbackDeltaTime;
                yield return null;
            }
            
            var component = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            component.m_AmplitudeGain = 0f;
            component.m_FrequencyGain = 0f;
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(_shakeCoroutine != null) Owner.StopCoroutine(_shakeCoroutine);
            var component = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            component.m_AmplitudeGain = 0f;
            component.m_FrequencyGain = 0f;
            return this;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public FeedbackScreenShake SetCamera(CinemachineVirtualCamera value)
        {
            camera = value;
            return this;
        }
        
        public FeedbackScreenShake SetIntensity(Vector2 value)
        {
            intensity.x = value.x;
            intensity.y = value.y;
            return this;
        }
        
        public FeedbackScreenShake SetFrequencyGain(Vector2 value)
        {
            frequencyGain.x = value.x;
            frequencyGain.y = value.y;
            return this;
        }
    }
}
