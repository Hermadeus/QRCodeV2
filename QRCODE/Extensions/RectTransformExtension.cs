using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.UI
{
    public static class RectTransformExtension
    {
        public static (float width, float heigth) GetSizePercentage(this RectTransform rectTransform, float percent)
        {
            percent = Mathf.Clamp(percent, 0f, 100f);
            percent /= 100f;

            return
                (rectTransform.sizeDelta.x * percent,
                    rectTransform.sizeDelta.y * percent);
        }
    }
}
