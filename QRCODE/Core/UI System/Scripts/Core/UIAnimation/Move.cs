using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [Serializable]
    public struct Move
    {
        [BoxGroup("Settings")]
        public AnimMove animMove;

        [BoxGroup("Settings")]
        public Direction direction;
        
        [BoxGroup("Settings")] 
        public float delayBefore;
        
        [BoxGroup("Settings")]
        public float duration;

        [BoxGroup("Settings")]
        public Ease ease;
        
        [BoxGroup("Settings")] 
        public bool useCurrentAsInitialPosition;

        [BoxGroup("Settings")]
        public Vector2 initialPosition;
    }

    public enum AnimMove
    {
        Translation,
        ScreenTranslation,
    }

    public enum Direction
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3,
        CustomPosition = 13
    }
}
