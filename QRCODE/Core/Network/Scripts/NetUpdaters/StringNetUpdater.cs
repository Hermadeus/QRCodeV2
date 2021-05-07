#if PHOTON_UNITY_NETWORKING

using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace QRCode.Network
{
    /// <summary>
    /// Update a value in Network when it's modified.
    /// </summary>
    public class StringNetUpdater : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, BoxGroup("Description"), TextArea(2,3)]
        private string description = default;
#endif

        //--<References>
        [SerializeField, BoxGroup("References")] 
        private PhotonView photonView = default;

        //--<Public>
        [BoxGroup("Settings")]
        public RpcTarget rpcTarget = RpcTarget.All;
        
        [BoxGroup("Events")]
        public NetUpdateUnityEvent onValueUpdated;

        //---<Public Properties>---------------------------------------------------------------------------------------<
        [ShowInInspector, BoxGroup("Debug"), ReadOnly, BoxGroup("Debug")]
        public string Value
        {
            get;
            private set;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        
        /// <summary>
        /// Set the value in network via RPC.
        /// </summary>
        /// <param name="newValue"></param>
        [Button]
        public void SetValue(string newValue)
        {
            photonView.RPC(nameof(RPC_SetValue), rpcTarget, newValue);
        }

        //---<RPC>-----------------------------------------------------------------------------------------------------<
        
        [PunRPC]
        void RPC_SetValue(string newValue)
        {
            Value = newValue;
            onValueUpdated?.Invoke(newValue);
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        [System.Serializable]
        public class NetUpdateUnityEvent : UnityEvent<string>{}
    }
}
#endif
