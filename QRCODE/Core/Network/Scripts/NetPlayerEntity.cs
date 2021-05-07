#if PHOTON_UNITY_NETWORKING

using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Network
{
    /// <summary>
    /// A concrete entity can be controlled by the player.
    /// </summary>
    public class NetPlayerEntity : MonoBehaviour, IPunObservable
    {
        //--<Static>
        public static NetPlayerEntity LocalPlayer;
        
        //--<Privates>
        [SerializeField, BoxGroup("References")]
        private PhotonView mPhotonView = default;

        [SerializeField, BoxGroup("References")] 
        private MeshRenderer meshRenderer = default;

        //---<PUBLIC PROPERTIES>---------------------------------------------------------------------------------------<
        public PhotonView PhotonView
        {
            get => mPhotonView;
        }
        
        public NetPlayer NetPlayer { get; set; }

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        private void Start()
        {
            if (Equals(PhotonNetwork.LocalPlayer, PhotonView.Controller)) LocalPlayer = this;
            NetPlayersManager.Instance.RegisterNetPlayer(this, PhotonView);
            if(PhotonNetworkManager.Instance.NetworkSettings.setPlayerColorsOnStart)
                meshRenderer.material.color = NetPlayer.Color;
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                
            }
            else
            {
                
            }
        }
    }
}
#endif