#if PHOTON_UNITY_NETWORKING

using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace QRCode.Network
{
    public class NetUpdater<T> : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, BoxGroup("Description"), TextArea(2,3)]
        private string description = "empty";
#endif

        [SerializeField, BoxGroup("References")] 
        private PhotonView photonView = default;
        
        [ShowInInspector, BoxGroup("Debug"), ReadOnly, BoxGroup("Debug")]
        public T Value
        {
            get;
            private set;
        }

        [SerializeField, BoxGroup("Settings")]
        private RpcTarget rpcTarget = RpcTarget.All;
        
        [System.Serializable]
        public class NetUpdateUnityEvent : UnityEvent<T>{}
        
        [BoxGroup("Events")]
        public NetUpdateUnityEvent onValueUpdated;

        [Button]
        public void SetValue(T newValue)
        {
            photonView.RPC(nameof(RPC_SetValue), rpcTarget, newValue);
        }

        [PunRPC]
        void RPC_SetValue(T newValue)
        {
            Value = newValue;
            onValueUpdated?.Invoke(newValue);
        }
    }
}

#endif
