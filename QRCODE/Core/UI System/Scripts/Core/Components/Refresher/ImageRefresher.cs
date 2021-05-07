using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QRCode.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageRefresher : UIRefresher
    {
        public Image ImageToRefresh = default;
        
        public override void Refresh(EventArgs args)
        {
            if (args is ImageEventArgs cast)
            {
                ImageToRefresh.sprite = cast.Sprite;
            }
        }

        private void Reset()
        {
            ImageToRefresh = GetComponent<Image>();
        }
    }

    public class ImageEventArgs : EventArgs
    {
        public Sprite Sprite { get; set; }
    }
}
