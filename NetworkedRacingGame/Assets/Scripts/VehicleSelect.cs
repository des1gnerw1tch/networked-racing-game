using UnityEngine;

/// <summary>
/// Holds the users current vehicle.
/// </summary>
public class VehicleSelect : MonoBehaviour
{
    [SerializeField] private GameObject defaultVehiclePrefab;
    
    public static VehicleSelect Instance { get; private set; }
    
    [SerializeField] private GameObject currentVehiclePrefab;
    
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

    public GameObject GetCurrentVehicle() => currentVehiclePrefab ?? defaultVehiclePrefab;

    public void SetCurrentVehicle(GameObject vehicle)
    {
        currentVehiclePrefab = vehicle;
    }
}
