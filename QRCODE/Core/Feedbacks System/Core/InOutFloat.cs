using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    [Serializable]
    public class InOut<T>
    {
        [Title("Amplitude")]
        public T amplitude;

        [Title("Durations"), Min(0f)] 
        public float inDuration;
        [Min(0f)]
        public float
            betweenDuration,
            outDuration;

        [Title("Easing")] 
        public Ease inEase;
        public Ease outEase;
        
        public InOut(T amplitude)
        {
            this.amplitude = amplitude;
            inDuration = betweenDuration = outDuration = .2f;
            inEase = Ease.InSine;
            outEase = Ease.OutSine;
        }
    }
    
    /// <summary>
    /// InOut Property with easing and state durations.
    /// </summary>
    [Serializable]
    public class InOutFloat : InOut<float>
    {
        public InOutFloat(float amplitude = 2f) : base(amplitude)
        {
            
        }
    }
    
    /// <summary>
    /// InOut Property with easing and state durations.
    /// </summary>
    [Serializable]
    public class InOutColor : InOut<Color>
    {
        public InOutColor(Color amplitude) : base(amplitude)
        {
            
        }
    }
}
