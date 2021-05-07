#if PHOTON_UNITY_NETWORKING

using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace QRCode.Network
{
    public class NetPlayersManager : MonoBehaviourPunCallbacks
    {
        public static NetPlayersManager Instance;
        
        [SerializeField, BoxGroup("References")] 
        private GameObject NetPlayerEntityPrefab = default;

        [SerializeField, BoxGroup("References")] 
        private PhotonNetworkSettings photonNetworkSettings = default;

        [ShowInInspector, BoxGroup("Debug")] 
        public Dictionary<string, NetPlayer> NetPlayers = new Dictionary<string, NetPlayer>();

        public Player LocalPlayer => PhotonNetwork.LocalPlayer;
        public string LocalUserId => LocalPlayer.UserId;
        public NetPlayer LocalNetPlayer => NetPlayers[LocalUserId];

        [NonSerialized] public Transform playerFolder;

        [FoldoutGroup("Events")] [SerializeField, FoldoutGroup("Events/Players Management")]
        public UnityEvent
            onJoiningRoom,
            onLeavingRoom;
        public PlayerUnityEvent
            onPlayerEnteredRoom,
            onPlayerLeftRoom;
        
        [SerializeField, FoldoutGroup("Events/Player Update")]
        public StringUnityEvent onPlayerNameUpdated;

        private void Awake()
        {
            Instance = this;
            LocalPlayer.NickName = "DefaultName";

            playerFolder = new GameObject("Net Players Entities").transform;
            playerFolder.SetParent(transform);
        }

        public override void OnJoinedRoom()
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                NetPlayers.Add(player.UserId, new NetPlayer(player));
            }

            if (NetPlayers.Count <= photonNetworkSettings.playerColors.Length)
                SetNetPlayerColor(photonNetworkSettings.playerColors[NetPlayers.Count - 1]);

            var playerObj =
                PhotonNetwork.Instantiate(NetPlayerEntityPrefab.name, transform.position, Quaternion.identity);
            
            onJoiningRoom?.Invoke();
        }

        public override void OnLeftRoom()
        {
            NetPlayers.Clear();
            onLeavingRoom?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debugging($"A Player join Room : name : {newPlayer.NickName}");
            NetPlayers.Add(newPlayer.UserId, new NetPlayer(newPlayer));
            SetNetPlayerColor(LocalNetPlayer.Color, newPlayer);
            
            onPlayerEnteredRoom?.Invoke(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debugging($"A Player left Room : name : {otherPlayer.NickName}");
            NetPlayers.Remove(otherPlayer.UserId);
            onPlayerLeftRoom?.Invoke(otherPlayer);
        }

        /// <summary>
        /// Register a net player entity in <see cref="NetPlayers"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="view"></param>
        public void RegisterNetPlayer(NetPlayerEntity entity, PhotonView view)
        {
            var player = NetPlayers[view.Controller.UserId];
            player.Entity = entity;
            player.PhotonView = view;
            entity.NetPlayer = player;
            entity.name = $"Player [{view.Controller.NickName}]";
            if (view.Controller.IsLocal) entity.name += $" (Local)";
            NetPlayers[view.Controller.UserId].Entity.transform.SetParent(playerFolder);
            playerFolder.name = $"Net Players Entities [{PhotonNetwork.PlayerList.Length}]";
        }

        public void SetNetPlayerColor(Color color, RpcTarget rpcTarget = RpcTarget.All)
        {
            photonView.RPC(nameof(RPC_SetNetPlayerColor), rpcTarget, color, LocalUserId);
        }

        public void SetNetPlayerColor(Color color, Player target)
        {
            photonView.RPC(nameof(RPC_SetNetPlayerColor), target, color, LocalUserId);
        }

        [Button]
        public void SetLocalPlayerName(string newName)
        {
            LocalPlayer.NickName = newName;
            onPlayerNameUpdated?.Invoke(newName);
        }

        [PunRPC]
        private void RPC_SetNetPlayerColor(Color color, string userID)
        {
            NetPlayers[userID].Color = color;
        }

        private void Debugging(object message)
        {
            Debug.Log($"<color=cyan> PHOTON : </color>" + message);
        }
    }
    
    public class PlayerUnityEvent : UnityEvent<Player>{}
}
#endif