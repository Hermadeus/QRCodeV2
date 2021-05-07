using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.UI;
using TMPro;
using UnityEngine;

namespace QRCode.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUGUIRefresher : UIRefresher
    {
        public TextMeshProUGUI textToUpdate = default;
        
        public override void Refresh(EventArgs args)
        {
            if (args is TextMeshProEventArgs cast)
            {
                textToUpdate.SetText(cast.value);
            }
        }

        private void Reset()
        {
            textToUpdate = GetComponent<TextMeshProUGUI>();
        }
    }

    public class TextMeshProEventArgs : EventArgs
    {
        public string value { get; set; }
    }
}
