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
    public Color playerColor;
    public List<InventoryItem> itemList;
    public int currentStatus;
    public bool wonTheGame;


    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
            GameInstance.characterName = "";
            GameInstance.gameDifficulty = "Easy";
            GameInstance.playerColor = new Color32(redColor, blueColor, 0, 255);
            GameInstance.gameTime = 5;
            GameInstance.numberOfCustomers = 1;
            GameInstance.created = false;
            GameInstance.itemList = new List<InventoryItem>();
            GameInstance.currentStatus = 1;
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
