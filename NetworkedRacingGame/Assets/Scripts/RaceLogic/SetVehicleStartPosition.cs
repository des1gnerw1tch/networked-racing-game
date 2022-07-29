using UnityEngine;
using Photon.Pun;

namespace RaceLogic
{
    /// Setup cars at beginning of race:
    /// 1. Each car calls RPC to request their position. Only sends to master client. Also turn CarController off. 
    /// 2. Master client will keep list of available spots (TrackNetworkManager). Will send position of first available spot when receive request, and then mark that spot taken
    /// 3. Each client will receive position data and move to that spot
    /// 4. Master client starts countdown to enable car controller
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
        
        // Only called on Master Client
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
