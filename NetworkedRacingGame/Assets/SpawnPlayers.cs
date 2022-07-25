using Photon.Pun;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject spawnPosition;

    private void Start()
    {
        PhotonNetwork.Instantiate(carPrefab.name, spawnPosition.transform.position, Quaternion.identity);
    }
    
}
