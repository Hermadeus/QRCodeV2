#if PHOTON_UNITY_NETWORKING

using QRCode.Extensions;
using UnityEngine;

using Sirenix.OdinInspector;

namespace QRCode.Network
{
    [CreateAssetMenu(menuName = "QRCode/PhotonNetwork/Data/Network Data")]
    public class PhotonNetworkSettings : SerializedScriptableObject
    {
        [BoxGroup("Settings")]
        public byte maxPlayerPerRoom = 4;

        [BoxGroup("Settings")] 
        public string gameVersion = "1";

        [FoldoutGroup("Settings/More Settings")]
        public bool setPlayerColorsOnStart = true;
        
        [FoldoutGroup("Settings/More Settings"), InlineButton("ResetColor")] public Color[] playerColors;
        
        void ResetColor()
        {
            playerColors = new Color[]
            {
                new Color().GetColorFromFrenchPallet(FrenchPallet.TURQUOISE),
                new Color().GetColorFromFrenchPallet(FrenchPallet.EMERALD),
                new Color().GetColorFromFrenchPallet(FrenchPallet.PETER_RIVER),
                new Color().GetColorFromFrenchPallet(FrenchPallet.AMETHYST),
                new Color().GetColorFromFrenchPallet(FrenchPallet.WET_ASPHALT),
                new Color().GetColorFromFrenchPallet(FrenchPallet.SUN_FLOWER),
                new Color().GetColorFromFrenchPallet(FrenchPallet.CARROT),
                new Color().GetColorFromFrenchPallet(FrenchPallet.ALIZARIN),
                new Color().GetColorFromFrenchPallet(FrenchPallet.CLOUDS),
                new Color().GetColorFromFrenchPallet(FrenchPallet.CONCRETE),
            };
        }
    }
}
#endif
