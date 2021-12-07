using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController GameInstance { get; private set; }
    public string characterName;
    public string gameDifficulty;
    public byte blueColor;
    public byte redColor;
    public byte greenColor;
    public float gameTime;
    public int numberOfCustomers;
    public bool created;
    public Color HUDColor;
    public List<InventoryItem> itemList;
    public int currentStatus;
    public int GainedSpeedUps;
    public bool wonTheGame;
    public int GunBurgerAmount;
    public int GunHotDogAmount;
    public int []HighestScore;
    public string[] topPlayer;
    public float playerSpeed;

    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
            GameInstance.characterName = "";
            GameInstance.gameDifficulty = "Easy";
            GameInstance.HUDColor = new Color32(0, 0, 0, 255);
            GameInstance.gameTime = 180;
            GameInstance.numberOfCustomers = 8;
            GameInstance.created = false;
            GameInstance.itemList = new List<InventoryItem>();
            GameInstance.currentStatus = 1;
            GameInstance.GainedSpeedUps = 0;
            GameInstance.GunBurgerAmount = 12;
            GameInstance.GunHotDogAmount = 12;
            GameInstance.playerSpeed = 10f;
            GameInstance.HighestScore = new int[3] {0,0,0};
            GameInstance.topPlayer = new string[3] { "", "", "" };
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
