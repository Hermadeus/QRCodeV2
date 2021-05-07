#if PHOTON_UNITY_NETWORKING

using System.Collections;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using Photon.Pun;
using Photon.Realtime;

namespace QRCode.Network
{
    public class PhotonNetworkManager : MonoBehaviourPunCallbacks
    {
        public static PhotonNetworkManager Instance;

        [SerializeField, FoldoutGroup("References")]
        private PhotonNetworkSettings networkSettings = default;

        [SerializeField, BoxGroup("Options")]
        private bool connectAutomatically = true;
        [SerializeField, BoxGroup("Options")]
        private string roomNameCreation = "RoomTest";

        [SerializeField, FoldoutGroup("Debug"), ReadOnly]
        private bool
            isConnected = false,
            inConnexion = false;

        [SerializeField, FoldoutGroup("Debug"), ReadOnly]
        private float waitingConnexionTime = 0f;

        [SerializeField, FoldoutGroup("Debug")]
        private bool debugMode = true;

        [FoldoutGroup("Events")]
        [FoldoutGroup("Events/Connexion Events")] public UnityEvent
            onConnexionToMasterSuccess,
            onDisconnectedToMaster,
            onWaitingConnexion;

        [FoldoutGroup("Events/Lobby Events")] public UnityEvent
            onJoinLobby,
            onLeftLobby;

        [FoldoutGroup("Events/Room Events")] public UnityEvent
            onJoinRoom,
            onLeftRoom,
            onCreationRoom;

        public PhotonNetworkSettings NetworkSettings => networkSettings;
        public string SetRoomNameCreation(string newName) => roomNameCreation = newName;
        public string GetRoomNameCreation => roomNameCreation;

        private void Awake()
        {
            Instance = this;
            CustomTypesPhotonSerialisation.Register();

            if (connectAutomatically)
            {
                ConnectUsingSettings();
            }
        }

        //---CONNEXION-------------------------------------
        
        [Button, FoldoutGroup("Manually Connexion")]
        public void ConnectUsingSettings()
        {
            if (isConnected)
            {
                Debugging($"You are already connected !");
                return;
            }
            
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = networkSettings.gameVersion;
            inConnexion = true;
            StartCoroutine(WaitingConnexionCor());
        }

        IEnumerator WaitingConnexionCor()
        {
            while (inConnexion)
            {
                onWaitingConnexion?.Invoke();
                waitingConnexionTime += Time.deltaTime;
                yield return null;
            }
            yield break;
        }

        public override void OnConnectedToMaster()
        {
            Debugging($"Connected to master : success in {waitingConnexionTime:0.00} sec.");
            isConnected = true;
            inConnexion = false;
            onConnexionToMasterSuccess?.Invoke();

            if (connectAutomatically)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        //---LOBBY-----------------------------------------
        
        public override void OnJoinedLobby()
        {
            Debugging($"Lobby joined : " +
                $"LobbyName : {PhotonNetwork.CurrentLobby.Name} | " +
                $"LobbyType : {PhotonNetwork.CurrentLobby.Type}");
            onJoinLobby?.Invoke();
            JoinOrCreateRoom();
        }

        public override void OnLeftLobby()
        {
            Debugging($"Lobby left");
            onLeftLobby?.Invoke();
        }
        
        //---ROOM------------------------------------------

        [Button, FoldoutGroup("Manually Connexion")]
        public void JoinOrCreateRoom()
        {
            var roomOptions = new RoomOptions()
            {
                IsVisible = true,
                MaxPlayers = networkSettings.maxPlayerPerRoom,
                PublishUserId = true,
            };
            PhotonNetwork.JoinOrCreateRoom(roomNameCreation, roomOptions, TypedLobby.Default);
        }

        [Button, FoldoutGroup("Manually Connexion")]
        public void JoinOrCreateRoom(string roomName)
        {
            roomNameCreation = roomName;
            JoinOrCreateRoom();
        }
        
        [Button, FoldoutGroup("Manually Connexion")]
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnCreatedRoom()
        {
            Debugging($"Room created : " +
                      $"RoomName : {PhotonNetwork.CurrentRoom.Name} | " +
                      $"PlayerCount : {PhotonNetwork.CurrentRoom.PlayerCount}");
            onCreationRoom?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            Debugging($"Room joined : " +
                $"RoomName : {PhotonNetwork.CurrentRoom.Name} | " +
                $"PlayerCount : {PhotonNetwork.CurrentRoom.PlayerCount}");
            onJoinRoom?.Invoke();
        }

        public override void OnLeftRoom()
        {
            Debugging($"Room left");
            onLeftRoom?.Invoke();
        }

        //---DISCONNECTION-----------------------------------
        
        [Button, FoldoutGroup("Manually Connexion")]
        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debugging($"Disconnected : <color=yellow> {cause} </color>.");
            onDisconnectedToMaster?.Invoke();
            isConnected = false;
        }

        private void Debugging(object message)
        {
            if (debugMode)
            {
                Debug.Log($"<color=cyan> PHOTON : </color>" + message);
            }
        }
    }
}
#endif
