using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TMP_InputField playerNameInput;

    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;

    private void Start()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    private void CreateRoom()
    {
        SetPlayerName();
        PhotonNetwork.CreateRoom(createInput.text);
    } 

    private void JoinRoom()
    { 
        SetPlayerName();
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom() => PhotonNetwork.LoadLevel("TrackSelect");

    private void SetPlayerName() => PlayerName.Instance.SetNickname(playerNameInput.text);
}
