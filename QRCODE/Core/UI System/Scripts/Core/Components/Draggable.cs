using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QRCode.UI
{
    public class Draggable : SerializedMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Canvas canvas = default;
        
        public RectTransform RectTransform => GetComponent<RectTransform>();
        
        public bool CanBeDrag { get; set; } = true;
        public bool IsDragged { get; set; } = false;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            IsDragged = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (CanBeDrag)
                RectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragged = false;
        }

        protected virtual void OnBeginDragCallback(PointerEventData eventData)
        {
            
        }
        
        protected virtual void OnDragCallback(PointerEventData eventData)
        {
            
        }
        
        protected virtual void OnEndDragCallback(PointerEventData eventData)
        {
            
        }
    }
}
