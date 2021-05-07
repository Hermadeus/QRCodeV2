using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace QRCode.UI
{
    //--<COMMENTARY>
    //Code founded on https://forum.unity.com/threads/linked-text-in-ugui-layout-groups.471477/
    
    [ExecuteInEditMode]
    public class DropCapParagraph : SerializedMonoBehaviour
    {
        //--<References>
        [SerializeField] private TextMeshProUGUI DropCap = default;
        [SerializeField] private TextMeshProUGUI RightText = default;
        [SerializeField] private TextMeshProUGUI BelowText = default;
        
        //--<Privates>
        [TextArea(5, 3), SerializeField] private string paragraph;

        private char dropCapChar;
        private string textStr;

        private bool IsValid => paragraph != null && paragraph.Length > 0 && DropCap && RightText && BelowText;

        //---<PROPERTIES>----------------------------------------------------------------------------------------------<
        public string Paragraph
        {
            get { return paragraph; }
            private set {
                paragraph = value;
                Refresh();
            }
        }

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        private void Start ()
        {
            Refresh();
        }

        private void OnEnable()
        {
            if (IsValid)
            {
                // RightText needs to be in Overflow mode for 'isTextOverflowing' / 'firstOverflowCharacterIndex' to work.
                RightText.overflowMode = TextOverflowModes.Overflow;
                BelowText.overflowMode = TextOverflowModes.Overflow;
                Refresh();
            }
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<

        public string SetParagraph(string paragraph)
        {
            Paragraph = paragraph;
            Refresh();
            return Paragraph;
        }
        
        private void Refresh()
        {
            if (!IsValid)
                return;

            // Assign the 1st char of paragraph to the dropcap component and the rest of the paragraph to the 'RightText' component.
            dropCapChar = paragraph.ToCharArray()[0];
            DropCap.text = dropCapChar.ToString();
            textStr = paragraph.Substring(1, paragraph.Length - 1);
            RightText.text = textStr;

            // This call causes the RightText TextMeshProUGUI component to do some form of layout which make 'isTextOverflowing' /
            // 'firstOverflowCharacterIndex' consistent with the text assigned above.
            RightText.Rebuild(CanvasUpdate.PreRender);

            if (RightText.isTextOverflowing)
            {
                var txt = RightText.text;
                var idx = RightText.firstOverflowCharacterIndex;
                RightText.text = txt.Substring(0, idx - 1);
                RightText.alignment = TextAlignmentOptions.Flush;
                BelowText.text = idx >= txt.Length ? "" : txt.Substring(idx);
            }
            else
            {
                RightText.alignment = TextAlignmentOptions.Justified;
                BelowText.text = "";
            }
        }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public void SetDropCapStyle(Color dropCapColor, TMP_FontAsset fontAsset = null)
        {
            DropCap.color = dropCapColor;
            if (fontAsset != null)
            {
                DropCap.font = fontAsset;
            }
        }
        
        public void SetRigthTextStyle(Color textColor, TMP_FontAsset fontAsset = null)
        {
            RightText.color = textColor;
            if (fontAsset != null)
            {
                RightText.font = fontAsset;
            }
        }
        
        public void SetBellowTextStyle(Color textColor, TMP_FontAsset fontAsset = null)
        {
            BelowText.color = textColor;
            if (fontAsset != null)
            {
                BelowText.font = fontAsset;
            }
        }

        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/QRCode/UI/Drop Cap Paragraph", false, 20)]
        private static void Create(MenuCommand menuCommand)
        {
            var obj = Resources.Load(UISettings.DROP_CAP_PREFAB_PATH);
            var go = Instantiate(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
        
        private void OnValidate()
        {
            Refresh();
        }
#endif
    }
}
