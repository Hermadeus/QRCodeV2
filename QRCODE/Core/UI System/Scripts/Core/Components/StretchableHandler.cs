using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QRCode.UI
{
    public class StretchableHandler : SerializedMonoBehaviour,
        IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Stretchable Stretchable;

        [SerializeField] private StretchDirection StretchDirection;

        [SerializeField] private Color
            normalColor = Color.clear,
            hoverColor = new Color(230f/255f, 126/255f, 34/255f, .2f);
        
        private Vector2 initialAncMin = new Vector2();
        private Vector2 initialAncMax = new Vector2();
        private Vector2 initialPivot = new Vector2();
        private Image image;

        private bool isDragging = false;
        
        private void Start()
        {
            if (TryGetComponent(out image))
                image.color = normalColor;
            
            
            initialAncMin = Stretchable.RectTransform.anchorMin;
            initialAncMax = Stretchable.RectTransform.anchorMax;
            initialPivot = Stretchable.RectTransform.pivot;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            switch (StretchDirection)
            {
                case StretchDirection.Right:
                    /*Stretchable.RectTransform.anchoredPosition += new Vector2(1920 / 2f, 0f);
                    Stretchable.RectTransform.anchorMin = new Vector2(0f, .5f);
                    Stretchable.RectTransform.anchorMax = new Vector2(0f, .5f);
                    Stretchable.RectTransform.pivot = new Vector2(0f, .5f);*/
                    break;
                case StretchDirection.Left:
                    Stretchable.RectTransform.pivot = new Vector2(.5f, 0f);
                    break;
                case StretchDirection.Up:
                    break;
                case StretchDirection.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            /*Stretchable.RectTransform.anchorMin = initialAncMin;
            Stretchable.RectTransform.anchorMax = initialAncMax;
            Stretchable.RectTransform.pivot = initialPivot;
            Stretchable.RectTransform.anchoredPosition -= new Vector2(1920 / 2f, 0f);*/
            Stretchable.CheckOnDragEnd();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var p = GetComponentInParent<Canvas>().MousePositionInCanvas();
            //Stretchable.Stretch(new Vector2(p.x - Stretchable.RectTransform.sizeDelta.y /2f, p.y), StretchDirection);
            Stretchable.RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, Stretchable.RectTransform.anchoredPosition.x, 300);
            //Stretchable.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 150);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            image?.DOColor(hoverColor, .2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!isDragging)
                image?.DOColor(normalColor, .2f);
        }
    }
}
