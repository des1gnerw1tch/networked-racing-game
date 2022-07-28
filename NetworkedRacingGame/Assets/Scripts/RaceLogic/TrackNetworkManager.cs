using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace RaceLogic
{
    /// <summary>
    /// Handles instantiation and destruction of vehicle prefab. Also handles navigation back to track select screen
    /// </summary>
    public class TrackNetworkManager : MonoBehaviour
    {
        [SerializeField] private PhotonView photonView;
        [SerializeField] private Button backToTrackSelectButton;
        [SerializeField] private StartingRacePosition[] startingRacePositions;

        private GameObject vehicleInstance;
        
        public static TrackNetworkManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                backToTrackSelectButton.onClick.AddListener(LoadTrackSelectScene);
            }
            else
            {
                backToTrackSelectButton.gameObject.SetActive(false);
            }
            
            string vehiclePrefab = VehicleSelect.Instance.GetCurrentVehicle().name;
            vehicleInstance = PhotonNetwork.Instantiate(vehiclePrefab, Vector3.zero, Quaternion.identity);
        }

        private void LoadTrackSelectScene()
        {
            photonView.RPC("RPC_LoadTrackSelectScene", RpcTarget.All);
        }

        [PunRPC]
        private void RPC_LoadTrackSelectScene() => PhotonNetwork.LoadLevel("TrackSelect");

        public Transform GetNextStartingPosition()
        {
            foreach (StartingRacePosition startingRacePosition in startingRacePositions)
            {
                if (startingRacePosition.spotTaken)
                {
                    continue;
                }

                startingRacePosition.spotTaken = true;
                return startingRacePosition.transform;
            }
            
            Debug.LogError("No more starting positions available! Returning null");
            return null;
        }

    }
}
