using UnityEngine;
using Photon.Pun;

namespace RaceLogic
{
    [RequireComponent(typeof(PhotonView))]
    public class SetVehicleStartPosition : MonoBehaviour
    {
        private PhotonView photonView;
        
        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            photonView.RPC("RPC_RequestVehicleStartingPosition", RpcTarget.MasterClient);
        }
        
        [PunRPC]
        private void RPC_RequestVehicleStartingPosition()
        {
            Transform startPosition = TrackNetworkManager.Instance.GetNextStartingPosition();
            photonView.RPC("RPC_SetVehicleStartingPosition", RpcTarget.All, startPosition.position, startPosition.rotation);
        }

        [PunRPC]
        private void RPC_SetVehicleStartingPosition(Vector3 pos, Quaternion rot)
        {
            if (photonView.IsMine)
            {
                transform.SetPositionAndRotation(pos, rot);
            }
        }
    }
}
