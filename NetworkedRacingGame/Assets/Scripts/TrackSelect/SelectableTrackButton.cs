using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class SelectableTrackButton : MonoBehaviour
{
    [SerializeField] private string trackName;
    [SerializeField] private Button button;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            button.onClick.AddListener(TrackSelected);
        }
    }

    private void TrackSelected() => PhotonView.Get(this).RPC("RPC_TrackSelected", RpcTarget.All);

    [PunRPC]
    private void RPC_TrackSelected() => PhotonNetwork.LoadLevel(trackName);
}
