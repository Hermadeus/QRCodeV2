using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Feedbacks
{
    public static class FeedbackMaker
    {
        private static bool isInit = false;

        private static FeedbackGeneralSettings _generalSettings;
        public static FeedbackGeneralSettings GeneralSettings
        {
            get
            {
                if (_generalSettings == null)
                    _generalSettings = FeedbackGeneralSettings.Instance;
                return _generalSettings;
            }
        }
        
        public static float FeedbackDeltaTime
        {
            get
            {
                return Time.deltaTime * FeedbackTimeScale;
            }
        }

        static private float feedbackTimeScale = 1f;
        public static float FeedbackTimeScale
        {
            get
            {
                return feedbackTimeScale;
            }
            set
            {
                value = Mathf.Min(value, 0f);
                feedbackTimeScale = value;
            }
        }
        
        public static void Init()
        {
            if (!isInit)
            {
                isInit = true;
            }
        }
        
        public static Sequencer MakeSequence(this MonoBehaviour owner)
        {
            Init();
            
            var sequencer = new Sequencer(owner);

            return sequencer;
        }

        public static T MakeFeedback<T>(this MonoBehaviour owner) where T : Feedback, new()
        {
            Init();

            var feedback = new T {Owner = owner};

            return feedback;
        }

        public static Sequencer Append(this Sequencer sequencer, Feedback feedback)
        {
            sequencer.Append(feedback);
            return sequencer;
        }

        public static Feedback Play(this Feedback feedback)
        {
            feedback.Play();
            return feedback;
        }
    }

    public static class PlayFeedback
    {
        private static FeedbackScreenShake ScreenShake { get; set; }
        private static FeedbackFreezeFrame FreezeFrame { get; set; }

        public static FeedbackFreezeFrame DoFreezeFrame(this MonoBehaviour owner, float duration = .5f)
        {
            FreezeFrame = GetNewFreezeFrame(owner, duration);
            FreezeFrame.Play();
            return FreezeFrame;
        }

        public static FeedbackFreezeFrame GetNewFreezeFrame(this MonoBehaviour owner, float duration = .5f)
        {
            return FreezeFrame ??= new FeedbackFreezeFrame(owner, duration);
        }
        
        public static FeedbackScreenShake DoScreenShake(this MonoBehaviour owner, CinemachineVirtualCamera camera , float intensity = 1f, float frequency = 1f, float duration = 1f)
        {
            ScreenShake = GetNewScreenShake(owner, camera, intensity, frequency, duration);
            ScreenShake.Play();
            return ScreenShake;
        }
        
        public static FeedbackScreenShake GetNewScreenShake(this MonoBehaviour owner, CinemachineVirtualCamera camera , float intensity = 1f, float frequency = 1f, float duration = .2f)
        {
            ScreenShake ??= new FeedbackScreenShake(owner, camera);

            ScreenShake.camera = camera;
            ScreenShake.Duration = duration;
            ScreenShake.frequencyGain.x = frequency;
            ScreenShake.frequencyGain.y = frequency;
            ScreenShake.intensity.x = intensity;
            ScreenShake.intensity.y = intensity;
            return ScreenShake;
        }
    }
}
