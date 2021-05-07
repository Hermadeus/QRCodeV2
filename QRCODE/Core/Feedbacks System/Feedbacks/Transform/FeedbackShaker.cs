using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackShaker : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public Transform Target ;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public TransformTarget Type = TransformTarget.Position;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool ResetPositionOnComplete = true;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float PositionStrength = 1f;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public int PositionVibrato = 10;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool ResetRotationOnComplete = true;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float RotationStrength = 1f;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public int RotationVibrato = 10;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool ResetScaleOnComplete = true;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float ScaleStrength = 1f;
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public int ScaleVibrato = 10;

        //--<Private>
        private Tween pTween, rTween, sTween;
        private Vector3 initPos, initScale;
        private Quaternion initRot;

        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<
        public FeedbackShaker() : base()
        {
            Type = TransformTarget.Position | TransformTarget.Rotation | TransformTarget.Scale;
            PositionStrength = 1f;
            PositionVibrato = 10;
            RotationStrength = 15f;
            RotationVibrato = 10;
            ScaleStrength = 1f;
            ScaleVibrato = 10;
            ResetPositionOnComplete = ResetRotationOnComplete = ResetScaleOnComplete = true;
        }

        public FeedbackShaker(MonoBehaviour owner, Transform target, 
            TransformTarget type = TransformTarget.Position | TransformTarget.Rotation | TransformTarget.Scale) 
            : base(owner)
        {
            Target = target;
            Type = type;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            initPos = Target.position;
            initRot = Target.rotation;
            initScale = Target.localScale;
            
            switch (Type)
            {
                case TransformTarget.Null:
                    break;
                case TransformTarget.Position:
                    pTween = Target.DOShakePosition(Duration, PositionStrength, PositionVibrato);
                    break;
                case TransformTarget.Rotation:
                    rTween = Target.DOShakeRotation(Duration, RotationStrength, RotationVibrato);
                    break;
                case TransformTarget.Scale:
                    sTween = Target.DOShakeScale(Duration, ScaleStrength, ScaleVibrato);
                    break;
                case TransformTarget.Position | TransformTarget.Rotation:
                    pTween = Target.DOShakePosition(Duration, PositionStrength, PositionVibrato);
                    rTween = Target.DOShakeRotation(Duration, RotationStrength, RotationVibrato);
                    break;
                case TransformTarget.Position | TransformTarget.Scale:
                    pTween = Target.DOShakePosition(Duration, PositionStrength, PositionVibrato);
                    sTween = Target.DOShakeScale(Duration, ScaleStrength, ScaleVibrato);
                    break;
                case TransformTarget.Rotation | TransformTarget.Scale:
                    rTween = Target.DOShakeRotation(Duration, RotationStrength, RotationVibrato);
                    sTween = Target.DOShakeScale(Duration, ScaleStrength, ScaleVibrato);
                    break;
                case TransformTarget.Position | TransformTarget.Rotation | TransformTarget.Scale:
                    pTween = Target.DOShakePosition(Duration, PositionStrength, PositionVibrato);
                    rTween = Target.DOShakeRotation(Duration, RotationStrength, RotationVibrato);
                    sTween = Target.DOShakeScale(Duration, ScaleStrength, ScaleVibrato);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();
            pTween?.Kill();
            rTween?.Kill();
            sTween?.Kill();
            return this;
        }

        //---<CALLBACKS>-----------------------------------------------------------------------------------------------<
        protected override void OnComplete()
        {
            base.OnComplete();
            if (ResetPositionOnComplete)
                Target.position = initPos;
            if (ResetRotationOnComplete)
                Target.rotation = initRot;
            if (ResetScaleOnComplete)
                Target.localScale = initScale;
        }
    }
    
    [Flags]
    public enum TransformTarget
    {
        Null = 0,
        Position = 1,
        Rotation = 2,
        Scale = 4
    }
}

