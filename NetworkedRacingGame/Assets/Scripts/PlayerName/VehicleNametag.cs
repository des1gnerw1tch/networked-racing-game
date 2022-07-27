using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class VehicleNametag : MonoBehaviour
{
    [SerializeField] private GameObject nametagObject;
    [SerializeField] private TMP_Text nametag;

    private void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        
        if (photonView.IsMine)
        {
            nametagObject.SetActive(false);
        }
        
        photonView.RPC("RPC_SetNameTag", RpcTarget.All, PlayerName.Instance.GetNickname());
    }

    [PunRPC]
    private void RPC_SetNameTag(string nickname) => nametag.text = nickname;
}
