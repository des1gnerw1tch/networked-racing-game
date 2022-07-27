using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ChooseVehicleHelperUI : MonoBehaviour
{
    [SerializeField] private Button humBButton;
    [SerializeField] private GameObject humB;
    
    [SerializeField] private Button roverButton;
    [SerializeField] private GameObject rover;
    
    [SerializeField] private Button hotDogButton;
    [SerializeField] private GameObject hotDog;
    
    private void Start()
    {
        humBButton.onClick.AddListener(delegate { ChangeVehicle(humB, humBButton); });;
        roverButton.onClick.AddListener( delegate { ChangeVehicle(rover, roverButton); });
        hotDogButton.onClick.AddListener( delegate { ChangeVehicle(hotDog, hotDogButton); });
    }

    private void ChangeVehicle(GameObject prefab, Button selectedButton)
    {
        VehicleSelect.Instance.SetCurrentVehicle(prefab);
        ResetButtonSizes();
        selectedButton.transform.localScale *= 1.2f;
    }

    private void ResetButtonSizes()
    {
        humBButton.transform.localScale = new Vector3(1, 1, 1);
        roverButton.transform.localScale = new Vector3(1, 1, 1);
        hotDogButton.transform.localScale = new Vector3(1, 1, 1);
    }
}
