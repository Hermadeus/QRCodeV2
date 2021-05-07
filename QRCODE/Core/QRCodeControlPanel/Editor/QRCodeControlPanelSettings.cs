using QRCode.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;

#if PHOTON_UNITY_NETWORKING
using QRCode.Network;
#endif

using QRCode.Audio;
using QRCode.Feedbacks;

namespace QRCode.ControlPanel
{
    [CreateAssetMenu(menuName = "QRCode/Control Panel/Options")]
    public class QRCodeControlPanelSettings : ScriptableObjectSingleton<QRCodeControlPanelSettings>
    {
        [TabGroup("Audio Settings"), InlineEditor(InlineEditorModes.GUIOnly, Expanded = true)]
        public SoundSystemSettings soundSystemSettings = default;

#if PHOTON_UNITY_NETWORKING
        [TabGroup("Network Settings"), InlineEditor(InlineEditorModes.GUIOnly, Expanded = true)]
        public PhotonNetworkSettings photonNetworkSettings = default;
#endif
        
        [TabGroup("Feedback Settings"), InlineEditor(InlineEditorModes.GUIOnly, Expanded = true)]
        public FeedbackGeneralSettings feedbackGeneralSettings = default;
    }
}
