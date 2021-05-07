using QRCode.Audio;
using QRCode.Feedbacks;
#if PHOTON_UNITY_NETWORKING
using QRCode.Network;
#endif
using UnityEngine;

namespace QRCode.ControlPanel
{
    public static class QRCodeControlPanelUtilities
    {
        //---<PATHS>---------------------------------------------------------------------------------------------------<
        private const string soundSystemSettingsPath = "Sound System Settings";
        private const string networkSettingsPath = "Data/Network Settings";
        private const string feedbackSettingsPath = "Feedback General Settings";

        //---<PROPERTIES>----------------------------------------------------------------------------------------------<
        private static FeedbackGeneralSettings feedbackGeneralSettings;
        public static FeedbackGeneralSettings FeedbackGeneralSettings
        {
            get
            {
                if (feedbackGeneralSettings == null)
                {
                    feedbackGeneralSettings = Resources.Load<FeedbackGeneralSettings>(feedbackSettingsPath);
                }

                return feedbackGeneralSettings;
            }
        }
#if PHOTON_UNITY_NETWORKING
        
        private static PhotonNetworkSettings photonNetworkSettings;
        public static PhotonNetworkSettings PhotonNetworkSettings
        {
            get
            {
                if (photonNetworkSettings == null)
                {
                    photonNetworkSettings = Resources.Load<PhotonNetworkSettings>(networkSettingsPath);
                }

                return photonNetworkSettings;
            }
        }
#endif
        
        private static SoundSystemSettings soundSystemSettings;
        public static SoundSystemSettings SoundSystemSettings
        {
            get
            {
                if (soundSystemSettings == null)
                {
                    soundSystemSettings = Resources.Load<SoundSystemSettings>(soundSystemSettingsPath);
                }

                return soundSystemSettings;
            }
        }
    }
}

