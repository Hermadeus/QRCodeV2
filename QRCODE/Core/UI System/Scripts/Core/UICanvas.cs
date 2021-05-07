using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    public class UICanvas : UIComponentBase
    {
        //--<Privates>
        private Canvas _canvas;

        //--<Public Properties>
        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                    _canvas = GetComponent<Canvas>();
                return _canvas;
            }
        }

        public Vector2 ScreenSize => RectTransform.sizeDelta;

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        public override void Init()
        {
            Canvas.enabled = true;
            base.Init();
        }

        //---<EDITOR>--------------------------------------------------------------------------------------------------<
        [Button]
        void Activate() => Canvas.enabled = true;
        
        [Button]
        void Desactive() => Canvas.enabled = false;
    }
}
