#if PHOTON_UNITY_NETWORKING

using Photon.Pun;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace QRCode.Network
{
    public class PlayerNameBillboard : MonoBehaviour
    {
        //--<Privates>
        [SerializeField, BoxGroup("References")] private StringNetUpdater stringNetUpdater;
        [SerializeField, BoxGroup("References")] private TextMeshProUGUI nameText = default;
        [SerializeField, BoxGroup("References")] private PhotonView m_PhotonView = default;
        [SerializeField, BoxGroup("Settings")] private bool hideIfItsMine = true;

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        private void Start()
        {
            SetPlayerNameOnBillboard(m_PhotonView.Controller.NickName);
            
            if (m_PhotonView.IsMine)
            {
                NetPlayersManager.Instance.onPlayerNameUpdated.AddListener(SetPlayerNameOnBillboard);
                if (hideIfItsMine)
                {
                    nameText.gameObject.SetActive(false);
                }
            }
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        [Button]
        public void SetPlayerNameOnBillboard(string newName) => stringNetUpdater.SetValue(newName);

        public void OnNameUpdated(string newName) => nameText.SetText(newName);
    }
}
#endif