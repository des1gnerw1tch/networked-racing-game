using UnityEngine;
using Photon.Pun;

namespace RaceLogic
{
    [RequireComponent(typeof(PhotonView))]
    public class SetVehicleStartPosition : MonoBehaviour
    {
        private void Start()
        {
            if (PhotonView.Get(this).IsMine)
            {
                PhotonView.Get(this).RPC("RPC_RequestVehicleStartingPosition", RpcTarget.MasterClient);
            }
        }
        
        [PunRPC]
        private void RPC_RequestVehicleStartingPosition()
        {
            Transform startPosition = TrackNetworkManager.Instance.GetNextStartingPosition();
            PhotonView.Get(this).RPC("RPC_SetVehicleStartingPosition", RpcTarget.All, startPosition.position, startPosition.rotation);
        }

        [PunRPC]
        private void RPC_SetVehicleStartingPosition(Vector3 pos, Quaternion rot)
        {
            if (PhotonView.Get(this).IsMine)
            {
                transform.SetPositionAndRotation(pos, rot);
            }
        }
    }
}
