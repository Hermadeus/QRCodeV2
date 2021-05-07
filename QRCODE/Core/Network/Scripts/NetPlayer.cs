#if PHOTON_UNITY_NETWORKING

using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Network
{
    /// <summary>
    /// This class is used to stock utils data of a player in network.
    /// </summary>
    [System.Serializable]
    public class NetPlayer
    {
        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<
        public NetPlayer(Player player)
        {
            this.Player = player;
        }
        
        //---<PUBLIC PROPERTIES>---------------------------------------------------------------------------------------<
        [ShowInInspector]
        public Player Player { get; set; }
        
        [ShowInInspector]
        public NetPlayerEntity Entity { get; set; }
        
        [ShowInInspector]
        public PhotonView PhotonView { get; set; }
        
        [ShowInInspector]
        public Color Color { get; set; }

        [ShowInInspector] 
        public string PlayerName => Player.NickName;

    }
}
#endif
