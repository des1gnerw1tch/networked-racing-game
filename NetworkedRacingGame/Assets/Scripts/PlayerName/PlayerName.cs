using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public static PlayerName Instance { get; private set; }
    private string nickname = "Guest";

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
    public string GetNickname() => nickname;
    public void SetNickname(string nickname) => this.nickname = nickname;
}
