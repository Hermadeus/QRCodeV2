using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    public abstract class UIRefresher : SerializedMonoBehaviour
    {
        public abstract void Refresh(EventArgs args);
    }
}
