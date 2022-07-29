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

        [SerializeField] private int lapsToComplete;
        private int lapsCompleted = 0;
        private int playersCompleted = 0;
        
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
            PhotonNetwork.Instantiate(vehiclePrefab, Vector3.zero, Quaternion.identity);
            GameCanvas.Instance.SetLapText(lapsCompleted, lapsToComplete);
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

        public void IncrementLap()
        {
            lapsCompleted++;
            
            GameCanvas.Instance.SetLapText(lapsCompleted, lapsToComplete);
            
            if (lapsCompleted == lapsToComplete)
            {
                photonView.RPC("RPC_PlayerFinishedRace", RpcTarget.MasterClient, PlayerName.Instance.GetNickname());
            }
        }
            
        // Only called on Master Client
        [PunRPC]
        private void RPC_PlayerFinishedRace(string playerNickname)
        {
            playersCompleted++;
            photonView.RPC("RPC_PlayerRaceFinishedData", RpcTarget.All, playerNickname, playersCompleted);
        }

        [PunRPC]
        private void RPC_PlayerRaceFinishedData(string playerNickname, int place)
        {
            if (place == 1)
            {
                GameCanvas.Instance.SetWinnerText(playerNickname);
            }
            
            if (playerNickname == PlayerName.Instance.GetNickname())
            {
                GameCanvas.Instance.SetPlaceText(place);
            }
            
        }

    }
}
