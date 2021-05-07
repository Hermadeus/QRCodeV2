using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace QRCode.UI
{
    public class Stretchable : SerializedMonoBehaviour
    {
        public RectTransform RectTransform => GetComponent<RectTransform>();

        public float xMinSize = 50f;
        public float xMaxSize = 750f;


        public void Stretch(Vector2 delta, StretchDirection direction)
        {
            switch (direction)
            {
                case StretchDirection.Right:
                    //RectTransform.DOSizeDelta(new Vector2(delta.x - RectTransform.anchoredPosition.x, RectTransform.sizeDelta.y), .1f);
                    //RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);

                    break;
                case StretchDirection.Left:
                    RectTransform.DOSizeDelta(new Vector2(delta.x - RectTransform.anchoredPosition.x, RectTransform.sizeDelta.y), .1f);
                    break;
                case StretchDirection.Up:
                    break;
                case StretchDirection.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void CheckOnDragEnd()
        {
            if (RectTransform.sizeDelta.x < xMinSize)
            {
                RectTransform.sizeDelta = new Vector2(xMinSize, RectTransform.sizeDelta.y);
            }

            if (RectTransform.sizeDelta.x > xMaxSize)
            {
                RectTransform.sizeDelta = new Vector2(xMaxSize, RectTransform.sizeDelta.y);
            }
        }

        [Button]
        public void ExtandSize(float sizeX, float sizeY, float duration = .1f, Ease ease = Ease.InOutSine)
        {
            RectTransform
                .DOSizeDelta(new Vector2(
                    RectTransform.sizeDelta.x + sizeX, 
                    RectTransform.sizeDelta.y), 
                    duration)
                .SetEase(ease);
        }       
    }

    public enum StretchDirection
    {
        Right,
        Left,
        Up,
        Down
    }
}