using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

/// <summary>
/// Handles instantiation and destruction of vehicle prefab. Also handles navigation back to track select screen
/// </summary>

public class TrackNetworkManager : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Button backToTrackSelectButton;
    private GameObject vehicleInstance;

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
        
        GameObject spawnPosition = GameObject.FindWithTag("SpawnPosition");
        string vehiclePrefab = VehicleSelect.Instance.GetCurrentVehicle().name;
        vehicleInstance = PhotonNetwork.Instantiate(vehiclePrefab, spawnPosition.transform.position, Quaternion.identity);
    }

    private void LoadTrackSelectScene()
    {
        photonView.RPC("RPC_LoadTrackSelectScene", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_LoadTrackSelectScene() => PhotonNetwork.LoadLevel("TrackSelect");
    
}
