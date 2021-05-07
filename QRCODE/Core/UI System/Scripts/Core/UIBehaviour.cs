using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [SerializeField]
    public class UIBehaviour
    {
        //--<Public>
        public UIAnimationPreset AnimationPreset = default;
        
        [HideIf("@this.AnimationPreset != null")]
        public UIAnimation Animation = new UIAnimation();

        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<
        public UIBehaviour()
        {
            Animation = new UIAnimation();
        }

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        public void Init()
        {
            if (AnimationPreset == null) return;
            
            Animation.canMove = AnimationPreset.canMove;
            Animation.canRotate = AnimationPreset.canRotate;
            Animation.canScale = AnimationPreset.canScale;
            Animation.canFade = AnimationPreset.canFade;
                
            Animation.move = AnimationPreset.move;
            Animation.rotate = AnimationPreset.rotate;
            Animation.scale = AnimationPreset.scale;
            Animation.fade = AnimationPreset.fade;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public UIAnimation PlayAnimation(UIComponentBase componentBase, Action onComplete = null)
        {
            Animation.Play(componentBase, this, onComplete);
            return Animation;
        }

        public UIAnimation StopAnimation()
        {
            Animation.StopMove();
            Animation.StopRotate();
            Animation.StopScale();
            Animation.StopFade();
            return Animation;
        }
    }
}
