using System;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI lapText;
    [SerializeField] private TextMeshProUGUI placeText;
    [SerializeField] private TextMeshProUGUI winnerText;
    
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
        winnerText.gameObject.SetActive(false);
    }

    public void SetLapText(int lap, int totalLaps) => lapText.text = "Lap: " + lap + "/" + totalLaps;

    public void SetPlaceText(int place) => placeText.text = "Place: " + place;

    public void SetWinnerText(string playerNickname)
    {
        winnerText.text = playerNickname + " is the WINNER!!!";
        winnerText.gameObject.SetActive(true);
    } 
}
