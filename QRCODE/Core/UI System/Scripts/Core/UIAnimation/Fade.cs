using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [Serializable]
    public struct Fade
    {
        [BoxGroup("Settings")] 
        public AnimFade animFade;

        [BoxGroup("Settings")] 
        public float delayBefore;
        
        [BoxGroup("Settings")] 
        public float duration;

        [BoxGroup("Settings")] 
        public Ease ease;

        [BoxGroup("Settings")] 
        public float initialAlpha;

        [BoxGroup("Settings")] 
        public float targetAlpha;
    }

    public enum AnimFade
    {
        FadeTo
    }
}
