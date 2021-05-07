using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [Serializable]
    public struct Rotate
    {
        [BoxGroup("Settings")]
        public AnimRotate animRotate;
        
        [BoxGroup("Settings")] 
        public float delayBefore;
        
        [BoxGroup("Settings")]
        public float duration;

        [BoxGroup("Settings")]
        public Ease ease;

        [BoxGroup("Settings")]
        public Vector3 initialRotation;
        
        [BoxGroup("Settings")]
        public Vector3 targetRotation;
    }

    public enum AnimRotate
    {
        SimplePingPong
    }
}
