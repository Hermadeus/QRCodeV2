using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    [CreateAssetMenu(menuName = "QRCode/Feedback/Settings")]
    public class FeedbackGeneralSettings : ScriptableObjectSingleton<FeedbackGeneralSettings>
    {
        [ShowInInspector] public Ease defaultEase = Ease.InOutSine;
    }
}