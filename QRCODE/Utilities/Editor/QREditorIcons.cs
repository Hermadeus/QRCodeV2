using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.Editor
{
    public static class QREditorIcons
    {
        public static Sprite GetEmoticonASprite(EditorEmoticon emoticon)
        {
            return emoticon switch
            {
                EditorEmoticon.SoundEmoticon => Resources.Load<Sprite>("Icons/GUI_Icon_SoundEmoticon"),
                EditorEmoticon.SettingEmoticon => Resources.Load<Sprite>("Icons/GUI_Icon_GearEmoticon"),
                EditorEmoticon.UIEmoticon => Resources.Load<Sprite>("Icons/GUI_Icon_UIEmoticon"),
                _ => throw new ArgumentOutOfRangeException(nameof(emoticon), emoticon, null)
            };
        }
    }

    public enum EditorEmoticon
    {
        SoundEmoticon,
        SettingEmoticon,
        UIEmoticon
    }
}