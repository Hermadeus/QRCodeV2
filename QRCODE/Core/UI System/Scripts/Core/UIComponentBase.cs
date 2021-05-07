using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    public class UIComponentBase : SerializedMonoBehaviour
    {
        //--<Privates>
        private RectTransform _rectTransform;
        private UICanvas _uiCanvas;
        private CanvasGroup _canvasGroup;
        
        //--<Public>
        [HideInInspector]
        public Vector2 initialPosition = new Vector3();

        //--<Public Properties>
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        public UICanvas UICanvas
        {
            get
            {
                if (_uiCanvas == null)
                    _uiCanvas = GetComponentInParent<UICanvas>();
                return _uiCanvas;
            }
        }
        
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_uiCanvas == null)
                    _canvasGroup = GetComponent<CanvasGroup>();

                return _canvasGroup;
            }
        }

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        protected virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            initialPosition = RectTransform.anchoredPosition;
        }
    }
}
