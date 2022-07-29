using UnityEngine;
using UnityEngine.UI;

// TODO: Rewrite this script to work like track select. Don't want to have to hard code in a new button every time. 
public class ChooseVehicleHelperUI : MonoBehaviour
{
    [SerializeField] private Button humBButton;
    [SerializeField] private GameObject humB;
    
    [SerializeField] private Button roverButton;
    [SerializeField] private GameObject rover;
    
    [SerializeField] private Button hotDogButton;
    [SerializeField] private GameObject hotDog;

    [SerializeField] private Button cadillacButton;
    [SerializeField] private GameObject cadillac;

    [SerializeField] private Button humCButton;
    [SerializeField] private GameObject humC;
    
    private void Start()
    {
        humBButton.onClick.AddListener(delegate { ChangeVehicle(humB, humBButton); });;
        roverButton.onClick.AddListener( delegate { ChangeVehicle(rover, roverButton); });
        hotDogButton.onClick.AddListener( delegate { ChangeVehicle(hotDog, hotDogButton); });
        cadillacButton.onClick.AddListener(delegate { ChangeVehicle(cadillac, cadillacButton); });
        humCButton.onClick.AddListener(delegate { ChangeVehicle(humC, humCButton); });
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
        cadillacButton.transform.localScale = new Vector3(1, 1, 1);
        humCButton.transform.localScale = new Vector3(1, 1, 1);
    }
}
