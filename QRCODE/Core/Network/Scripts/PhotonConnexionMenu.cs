#if PHOTON_UNITY_NETWORKING

using System.Collections;

using Photon.Pun;
using Photon.Realtime;

using Sirenix.OdinInspector;
using TMPro;
using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace QRCode.Network
{
    public class PhotonConnexionMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField, BoxGroup("References")]
        private CanvasGroup canvasGroup = default;

        [SerializeField, BoxGroup("References")]
        private TextMeshProUGUI
            roomNameInputField = default,
            message = default;
        [SerializeField, BoxGroup("References")]
        private Button
            connexionButton = default,
            joinOrCreateButton = default;

        [SerializeField, FoldoutGroup("Options")]
        private bool useThisMenu = true;

        private void Start()
        {
            roomNameInputField.SetText(PhotonNetworkManager.Instance.GetRoomNameCreation);
            if (useThisMenu) Show();
            else Hide();
        }

        public void ConnexionButton()
        {
            PhotonNetworkManager.Instance.ConnectUsingSettings();
            message.SetText("Connexion in progress...");
        }

        public void JoinOrCreateRoomButton()
        {
            PhotonNetworkManager.Instance.JoinOrCreateRoom();
            message.SetText("Joining room or creating room in progress...");
        }

        public void CheckRoomName(string input)
        {
            connexionButton.interactable = input.Length >= 3;
            PhotonNetworkManager.Instance.SetRoomNameCreation(input);
        }

        public override void OnConnectedToMaster()
        {
            joinOrCreateButton.interactable = true;
            message.SetText("Connected To Master.");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            if (useThisMenu)
            {
                Show();
                message.SetText("You have been disconnected, cause : <color=yellow>" + cause + "</color>");
            }
        }

        public override void OnJoinedRoom()
        {
            StartCoroutine(HideCoroutine());
            message.SetText("Room joined !");
        }

        private IEnumerator HideCoroutine()
        {
            yield return new WaitForSeconds(.5f);
            Hide();
        }

        public void Show()
        {
            canvasGroup.DOFade(1f, .2f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            canvasGroup.DOFade(0f, .2f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
#endif
