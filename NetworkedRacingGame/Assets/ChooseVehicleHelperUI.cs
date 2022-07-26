using UnityEngine;
using UnityEngine.UI;

public class ChooseVehicleHelperUI : MonoBehaviour
{
    [SerializeField] private Button humBButton;
    [SerializeField] private GameObject humB;
    
    private void Start()
    {
        humBButton.onClick.AddListener(HumBSelected);
    }

    private void HumBSelected() => VehicleSelect.Instance.SetCurrentVehicle(humB);
}
