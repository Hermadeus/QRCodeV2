using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QRCode.UI
{
    public class UISwipper : UIElement
    {
        //--<Public>
        [OnValueChanged("ResetPivot")]
        public RectTransform.Edge Edge = RectTransform.Edge.Left;

        [Range(0f,100f), OnValueChanged("UpdateSize")]
        public float sizeScreenPurcent = 0f;

        //--<Privates>
        private Tween showTween, hideTween;
        
        [HorizontalGroup("Show")]
        [SerializeField] private float showDuration = .2f;

        [HorizontalGroup("Show")] [SerializeField]
        private Ease showEase = Ease.InOutSine;
        
        [HorizontalGroup("Hide")]
        [SerializeField] private float hideDuration = .2f;

        [HorizontalGroup("Hide")] [SerializeField]
        private Ease hideEase = Ease.InOutSine;
        
        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        public override void Init()
        {
            if (hideOnStart)
            {
                Hide();
            }
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override void Show()
        {
            gameObject.SetActive(true);

            switch (Edge)
            {
                case RectTransform.Edge.Left:
                    RectTransform
                        .DOAnchorPos(new Vector2(0f, RectTransform.anchoredPosition.y), showDuration)
                        .SetEase(showEase);
                    break;
                case RectTransform.Edge.Right:
                    RectTransform
                        .DOAnchorPos(new Vector2(0f, RectTransform.anchoredPosition.y), showDuration)
                        .SetEase(showEase);
                    break;
                case RectTransform.Edge.Top:
                    RectTransform
                        .DOAnchorPos(new Vector2(RectTransform.anchoredPosition.x, 0f), showDuration)
                        .SetEase(showEase);
                    break;
                case RectTransform.Edge.Bottom:
                    RectTransform
                        .DOAnchorPos(new Vector2(RectTransform.anchoredPosition.x, 0f), showDuration)
                        .SetEase(showEase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            isVisible = true;
        }

        public override void Hide()
        {
            var sequence = DOTween.Sequence();
            
            switch (Edge)
            {
                case RectTransform.Edge.Left:
                    RectTransform
                        .DOAnchorPos(new Vector2(- RectTransform.sizeDelta.x, RectTransform.anchoredPosition.y),
                            hideDuration)
                        .SetEase(hideEase)
                        .OnComplete(() => gameObject.SetActive(false));
                    break;
                case RectTransform.Edge.Right:
                    RectTransform
                        .DOAnchorPos(new Vector2(RectTransform.sizeDelta.x, RectTransform.anchoredPosition.y),
                            hideDuration)
                        .SetEase(hideEase)
                        .OnComplete(() => gameObject.SetActive(false));
                    break;
                case RectTransform.Edge.Top:
                    RectTransform
                        .DOAnchorPos(new Vector2(RectTransform.anchoredPosition.x, RectTransform.sizeDelta.y),
                            hideDuration)
                        .SetEase(hideEase)
                        .OnComplete(() => gameObject.SetActive(false));
                    break;
                case RectTransform.Edge.Bottom:
                    RectTransform
                        .DOAnchorPos(new Vector2(RectTransform.anchoredPosition.x, - RectTransform.sizeDelta.y),
                            hideDuration)
                        .SetEase(hideEase)
                        .OnComplete(() => gameObject.SetActive(false));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            isVisible = false;
        }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        private void ResetPivot()
        {
            switch (Edge)
            {
                case RectTransform.Edge.Left:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, RectTransform.sizeDelta.x);
                    break;
                case RectTransform.Edge.Right:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, RectTransform.sizeDelta.x);
                    break;
                case RectTransform.Edge.Top:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, RectTransform.sizeDelta.y);
                    break;
                case RectTransform.Edge.Bottom:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, RectTransform.sizeDelta.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UpdateSize()
        {
            switch (Edge)
            {
                case RectTransform.Edge.Left:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, UICanvas.Canvas.RectTransform().GetSizePercentage(sizeScreenPurcent).width);
                    break;
                case RectTransform.Edge.Right:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, UICanvas.Canvas.RectTransform().GetSizePercentage(sizeScreenPurcent).width);
                    break;
                case RectTransform.Edge.Top:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, UICanvas.Canvas.RectTransform().GetSizePercentage(sizeScreenPurcent).heigth);
                    break;
                case RectTransform.Edge.Bottom:
                    RectTransform.SetInsetAndSizeFromParentEdge(Edge, 0, UICanvas.Canvas.RectTransform().GetSizePercentage(sizeScreenPurcent).heigth);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
