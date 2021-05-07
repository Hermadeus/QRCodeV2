using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [CreateAssetMenu(menuName = "QRCode/UI/Animation Preset")]
    public class UIAnimationPreset : SerializedScriptableObject
    {
        [FoldoutGroup("Move"), GUIColor(26f/255f, 188f/255f, 156f/255f, 1f)]
        public bool canMove = false;
        [FoldoutGroup("Move"), HideIf("@!this.canMove"), GUIColor(26f/255f, 188f/255f, 156f/255f, 1f)]
        public Move move;

        [FoldoutGroup("Rotate"), GUIColor(230f/255f, 126f/255f, 34f/255f, 1f)]
        public bool canRotate = false;
        [FoldoutGroup("Rotate"), HideIf("@!this.canRotate"), GUIColor(230f/255f, 126f/255f, 34f/255f, 1f)]
        public Rotate rotate;

        [FoldoutGroup("Scale"), GUIColor(231f/255f, 76f/255f, 60f/255f, 1f)]
        public bool canScale = false;
        [FoldoutGroup("Scale"), HideIf("@!this.canScale"), GUIColor(231f/255f, 76f/255f, 60f/255f, 1f)]
        public Scale scale;

        [FoldoutGroup("Fade"), GUIColor(208f/255f, 143f/255f, 235f/255f, 1f)]
        public bool canFade = false;
        [FoldoutGroup("Fade"), HideIf("@!this.canFade"), GUIColor(208f/255f, 143f/255f, 235f/255f, 1f)]
        public Fade fade;

        private void Reset()
        {
            //--<Move>
            canMove = UISettings.DEFAULT_CAN_MOVE;
            move.duration = UISettings.DEFAULT_MOVE_DURATION;
            move.ease = UISettings.DEFAULT_MOVE_EASE;
            move.animMove = UISettings.DEFAULT_ANIM_MOVE;
            move.useCurrentAsInitialPosition = UISettings.DEFAULT_USE_INITIAL_CURRENT_POSITION;

            //--<Rotate>
            canMove = UISettings.DEFAULT_CAN_ROTATE;
            rotate.duration = UISettings.DEFAULT_ROTATE_DURATION;
            rotate.ease = UISettings.DEFAULT_ROTATE_EASE;
            rotate.animRotate = UISettings.DEFAULT_ANIM_ROTATE;
            rotate.initialRotation = new Vector3();
            rotate.targetRotation = new Vector3(0f, 0f, 20f);
            
            //--<Scale>
            canScale = UISettings.DEFAULT_CAN_SCALE;
            scale.duration = UISettings.DEFAULT_SCALE_DURATION;
            scale.ease = UISettings.DEFAULT_SCALE_EASE;
            scale.animScale = UISettings.DEFAULT_ANIM_SCALE;
            scale.initialScale = new Vector3(1f, 1f, 1f);
            scale.targetScale = new Vector3();
            
            //--<Fade>
            canFade = UISettings.DEFAULT_CAN_FADE;
            fade.duration = UISettings.DEFAULT_FADE_DURATION;
            fade.ease = UISettings.DEFAULT_FADE_EASE;
            fade.animFade = UISettings.DEFAULT_ANIM_FADE;
            fade.initialAlpha = 1f;
            fade.targetAlpha = 0f;
        }
    }
}
