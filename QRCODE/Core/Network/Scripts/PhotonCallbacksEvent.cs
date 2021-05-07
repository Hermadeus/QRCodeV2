#if PHOTON_UNITY_NETWORKING

using Photon.Pun;
using Photon.Realtime;

using Sirenix.OdinInspector;

using UnityEngine.Events;

namespace QRCode.Network
{
    /// <summary>
    /// Some Event from <see cref="MonoBehaviourPunCallbacks"/>
    /// </summary>
    public class PhotonCallbacksEvent : MonoBehaviourPunCallbacks
    {
        [FoldoutGroup("Events")] 
        [FoldoutGroup("Events/Connexion Events")]
        public UnityEvent
            onConnexion,
            onDisconnexion;

        [FoldoutGroup("Events/Lobby Events")]
        public UnityEvent
            onJoinLobby,
            onLeftLobby;
        
        [FoldoutGroup("Events/Room Events")]
        public UnityEvent
            onJoinRoom,
            onLeftRoom;

        [FoldoutGroup("Events/Player Events")] public UnityEvent
            onPlayerEnteredRoom,
            onPlayerLeftRoom;
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            onConnexion?.Invoke();
        }

        public override void OnConnectedToMaster()
        {
            onDisconnexion?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            onJoinRoom?.Invoke();;
        }

        public override void OnLeftRoom()
        {
            onLeftRoom?.Invoke();
        }
        
        public override void OnJoinedLobby()
        {
            onJoinLobby?.Invoke();
        }
        
        public override void OnLeftLobby()
        {
            onLeftLobby?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            onPlayerEnteredRoom?.Invoke();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            onPlayerLeftRoom?.Invoke();
        }
    }
}
#endif
