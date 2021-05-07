using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace QRCode.UI
{
    public static class UISettings
    {
        //---<PREFABS>-------------------------------------------------------------------------------------------------<
        public const string DROP_CAP_PREFAB_PATH = "Drop Cap Text";
        
        //---<ANIMATIONS>----------------------------------------------------------------------------------------------<
        
        //--<Move>
        public const bool DEFAULT_CAN_MOVE = false;
        public const float DEFAULT_MOVE_DURATION = .2f;
        public const Ease DEFAULT_MOVE_EASE = Ease.InOutSine;
        public const AnimMove DEFAULT_ANIM_MOVE = AnimMove.Translation;
        public const bool DEFAULT_USE_INITIAL_CURRENT_POSITION = true;
        
        //--<Rotate>
        public const bool DEFAULT_CAN_ROTATE = false;
        public const float DEFAULT_ROTATE_DURATION = .2f;
        public const Ease DEFAULT_ROTATE_EASE = Ease.InOutSine;
        public const AnimRotate DEFAULT_ANIM_ROTATE = AnimRotate.SimplePingPong;
        
        //--<Scale>
        public const bool DEFAULT_CAN_SCALE = false;
        public const float DEFAULT_SCALE_DURATION = .2f;
        public const Ease DEFAULT_SCALE_EASE = Ease.InOutSine;
        public const AnimScale DEFAULT_ANIM_SCALE = AnimScale.Stretch;
        
        //--<Fade>
        public const bool DEFAULT_CAN_FADE = false;
        public const float DEFAULT_FADE_DURATION = .2f;
        public const Ease DEFAULT_FADE_EASE = Ease.InOutSine;
        public const AnimFade DEFAULT_ANIM_FADE = AnimFade.FadeTo;
    }
}
