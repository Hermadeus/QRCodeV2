using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [Serializable]
    public struct Scale
    {
        [BoxGroup("Settings")]
        public AnimScale animScale;

        [BoxGroup("Settings")] 
        public float delayBefore;
        
        [BoxGroup("Settings")]
        public float duration;

        [BoxGroup("Settings")]
        public Ease ease;

        [BoxGroup("Settings")]
        public Vector3 initialScale;

        [BoxGroup("Settings")]
        public Vector3 targetScale;
    }

    public enum AnimScale
    {
        Stretch
    }
}

