using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace QRCode.Feedbacks
{
    public class ScreenFlash : SerializedMonoBehaviour
    {
        public FeedbackScreenFlash flash = new FeedbackScreenFlash();

        private void Start()
        {
            flash.SetOwner(this);
        }

        [Button]
        public FeedbackScreenFlash Flash()
        {
            flash.Play();
            return flash;
        }

        [Button]
        public FeedbackScreenFlash Kill()
        {
            flash.Kill();
            return flash;
        }
    }
}
