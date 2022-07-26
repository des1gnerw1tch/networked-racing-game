using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject spawnPosition;

    private void Start()
    {
        string vehiclePrefab = VehicleSelect.Instance.GetCurrentVehicle().name;
        PhotonNetwork.Instantiate(vehiclePrefab, spawnPosition.transform.position, Quaternion.identity);
    }
    
}
