using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class PanelHandler : MonoBehaviour
{
    private GameObject Timer;
    private GameObject Customers;
    private GameObject HUDHolder;
    private Image HUDImage;
    private GameObject InventorySystem;
    private Image InventorySystemImage;
    private Image InventorySystemBurgerAmountImage;
    private Text InventorySystemBurgerAmountValue;
    private Image InventorySystemSpeedUpImage;
    private Text InventorySystemSpeedUpValue;
    private Image InventorySystemHotDogAmountImage;
    private Text InventorySystemHotDogAmountValue;
    private GameObject gameOverPanel;
    private GameObject WinningScreen;
    
    private Button gameOverReplayBtn;
    private Button GameOverbackBtn;
    private Button WinningScreenReplayBtn;
    private Button WinningScreenBackBtn;
    private Text RemainingTime;
    private Text RemainingCustomers;
    private Text PlayerName;
    private int minute;
    private int seconds;
    private float totalTime;
    private bool occuring;
    [SerializeField] Sprite Soda;

    private bool AlreadyEnded;
    // Start is called before the first frame update

    private GameObject gameInformation;              // To display the game information

    private void Awake()
    {
        startNewGameFunctionality();
        InitialiseWinningScreenAndButtons();
        InitialiseGameOverScreenAndButtons();
        InitialiseInventorySystemScreenAndButtons();

        InitialiseHUDTextAndButtons();
        CalculateRemainingTime();
        

    }

    

    void Start()
    {
        updatePlayerName();
        Time.timeScale = 1;
        occuring = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (GameController.GameInstance.itemList.Count > 2)
        {
            GameController.GameInstance.itemList.RemoveAt(GameController.GameInstance.itemList.Count - 1);
        }
        else
        {
            Debug.Log("Num elements " + GameController.GameInstance.itemList.Count);
        }



        gameInformation = GameObject.Find("Canvas_Information");       // gives gameInformation a reference to the canvas i want


    }

    

    /**
     * Shows player name on the screen
     */
    

    // Update is called once per frame
    void Update()
    {

        UpdateInventory();
        UpdateCustomer();
        GameWinnerFunctionality();
        GameOverConditionAndTimeFuctionality();
        OpenCloseHudeAndInventorySystem();

        // Displays the information panel to the player while 'i' is held down
        while (Input.GetKeyDown("i"))
        {
            gameInformation.SetActive(false);
        }





    }



















    private void updatePlayerName()
    {
        if (GameController.GameInstance.characterName != null)
        {
            PlayerName.text = "Welcome " + GameController.GameInstance.characterName;
        }
    }

    private void GameWinnerFunctionality()
    {
        if (GameController.GameInstance.numberOfCustomers <= 0)
        {
            if (!AlreadyEnded)
            {
                AlreadyEnded = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
                
            }
        }
    }
    private void GameOverConditionAndTimeFuctionality()
    {
        if (GameController.GameInstance.gameTime >= 0)
        {
            GameController.GameInstance.gameTime -= Time.deltaTime; ;
            totalTime = GameController.GameInstance.gameTime;
            CalculateRemainingTime();
        }
        else
        {
            if (!AlreadyEnded)
            {
                AlreadyEnded = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeInHierarchy);
            }

        }
    }

    /**
    * Handle the case when player wants to close the inventory system
    */
    private void OpenCloseHudeAndInventorySystem()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            HUDHolder.gameObject.SetActive(!HUDHolder.gameObject.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventorySystem.gameObject.SetActive(!InventorySystem.gameObject.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GameController.GameInstance.GainedSpeedUps--;
            if (GameController.GameInstance.GainedSpeedUps > -1 && occuring == false)
            {
               
                StartCoroutine("UseSpeedups");

            }

        }
    }

    private IEnumerator UseSpeedups()
    {
        
        
        occuring = true;
        GameController.GameInstance.playerSpeed *= 3;
        yield return new WaitForSeconds(20f);
        GameController.GameInstance.playerSpeed /= 3;
        occuring = false;

    }


    /**
     * Updates Time on the HUD
     */
    private void CalculateRemainingTime()
    {
        minute = (int)totalTime / 60;
        seconds = (int)totalTime % 60;
        if (seconds < 10)
        {
            RemainingTime.text = "Remaining Time: " + minute + ":0" + seconds;
        }
        else
        {
            RemainingTime.text = "Remaining Time: " + minute + ":" + seconds;
        }
        
    }
    /**
     * Updating number of cutomers
     */
    private void UpdateCustomer()
    { 
        RemainingCustomers.text = "Remaining Customers: " + (GameController.GameInstance.numberOfCustomers).ToString();
    }

    private void InitialiseGameOverScreenAndButtons()
    {
        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel").gameObject;
            gameOverReplayBtn = gameOverPanel.transform.Find("Replay").GetComponent<Button>();
            gameOverReplayBtn.onClick.AddListener(delegate { replayTheGame(gameOverPanel); });
            GameOverbackBtn = gameOverPanel.transform.Find("Back").GetComponent<Button>();
            GameOverbackBtn.onClick.AddListener(delegate { LoadSceneByName("Scene_Menu"); });
            gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeInHierarchy);
        }
        
    }

    private void InitialiseWinningScreenAndButtons()
    {
     
        if(WinningScreen == null)
        {
            WinningScreen = GameObject.Find("WonPanel").gameObject;
            WinningScreenReplayBtn = WinningScreen.transform.Find("Replay").GetComponent<Button>();
            WinningScreenReplayBtn.onClick.AddListener(delegate { replayTheGame(gameOverPanel); });
            WinningScreenBackBtn = WinningScreen.transform.Find("Back").GetComponent<Button>();
            WinningScreenBackBtn.onClick.AddListener(delegate { LoadSceneByName("Scene_Menu"); });
            WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
        }
        
    }
    private void InitialiseHUDTextAndButtons()
    {

        HUDHolder = GameObject.Find("HUDPanel").gameObject;
        Timer = HUDHolder.transform.Find("Timer").gameObject;
        Customers = HUDHolder.transform.Find("Customers").gameObject;
        HUDImage = HUDHolder.transform.GetComponent<Image>();
        HUDImage.color = GameController.GameInstance.HUDColor;
        PlayerName = HUDHolder.transform.Find("Name").GetComponent<Text>();
        RemainingTime = Timer.transform.Find("RemainingTime").GetComponent<Text>();
        RemainingCustomers = Customers.transform.Find("RemainingCustomers").GetComponent<Text>();
      
        
     
    }

    private void InitialiseInventorySystemScreenAndButtons()
    {
        InventorySystem = GameObject.Find("GameInventoryPanel").gameObject;
        InventorySystemImage = InventorySystem.transform.GetComponent<Image>();
        InventorySystemImage.color = new Color32(GameController.GameInstance.redColor, GameController.GameInstance.greenColor, GameController.GameInstance.blueColor, 255);
        InventorySystemSpeedUpImage = InventorySystem.transform.Find((3).ToString()).GetComponent<Image>();
        InventorySystemSpeedUpValue = InventorySystemSpeedUpImage.transform.Find((3+"T").ToString()).GetComponent<Text>();
        InventorySystemSpeedUpValue.text = "";
        InventorySystemSpeedUpImage.enabled = false;
        InventorySystemBurgerAmountImage = InventorySystem.transform.Find((1).ToString()).GetComponent<Image>();
        InventorySystemBurgerAmountValue = InventorySystemBurgerAmountImage.transform.Find((1 + "T").ToString()).GetComponent<Text>();
        InventorySystemHotDogAmountImage = InventorySystem.transform.Find((2).ToString()).GetComponent<Image>();
        InventorySystemHotDogAmountValue = InventorySystemHotDogAmountImage.transform.Find((2 + "T").ToString()).GetComponent<Text>();
    }
 
    private void UpdateInventory()
    {
        if (GameController.GameInstance.GainedSpeedUps > 0 && GameController.GameInstance.GainedSpeedUps < 3)
        {
            for (int i = 0; i < GameController.GameInstance.itemList.Count; i++)
            {
                if (GameController.GameInstance.itemList[i].name == "Speedups")
                {
                    InventorySystemSpeedUpImage.enabled = true;
                   // InventorySystemSpeedUpImage.color = Color.white;
                    // set image to speedups
                    InventorySystemSpeedUpImage.sprite = Soda;
                    // update the text
                    InventorySystemSpeedUpValue.text = (GameController.GameInstance.itemList[i].count).ToString();
                }

            }
        }

        InventorySystemBurgerAmountValue.text = GameController.GameInstance.GunBurgerAmount.ToString();
        InventorySystemHotDogAmountValue.text = GameController.GameInstance.GunHotDogAmount.ToString();
        if (GameController.GameInstance.GainedSpeedUps <= 0)
        {
            InventorySystemSpeedUpValue.text = "";
            GameController.GameInstance.GainedSpeedUps = 0;
            if (GameController.GameInstance.itemList.Count > 2)
            {
                GameController.GameInstance.itemList.RemoveAt(GameController.GameInstance.itemList.Count-1);
                //InventorySystemSpeedUpImage.sprite = null;
                InventorySystemSpeedUpImage.enabled = false;
            }
        }
        else
        {
            InventorySystemSpeedUpValue.text = GameController.GameInstance.GainedSpeedUps.ToString();
        }
        
    }
    private void replayTheGame(GameObject panel)
    {
        GameController.GameInstance.GainedSpeedUps = 0;
        InventorySystemSpeedUpValue.text = "";
        GameController.GameInstance.itemList.RemoveAt(GameController.GameInstance.itemList.Count - 1);
        InventorySystemSpeedUpImage.sprite = null;
        InventorySystemSpeedUpImage.enabled = false;
        panel.gameObject.SetActive(!panel.gameObject.activeInHierarchy);
        SceneManager.LoadScene("Scene_Chase");

    }

    public void LoadSceneByName(string sceneName)
    {
        GameController.GameInstance.GainedSpeedUps=0;
        InventorySystemSpeedUpValue.text = "";
        GameController.GameInstance.itemList.RemoveAt(GameController.GameInstance.itemList.Count - 1);
        InventorySystemSpeedUpImage.sprite = null;
        InventorySystemSpeedUpImage.enabled = false;
        SceneManager.LoadScene(sceneName);
    }

    private void startNewGameFunctionality()
    {
        
        if (GameController.GameInstance.numberOfCustomers <= 0)
        {
            GameController.GameInstance.playerSpeed = 10f;
            GameController.GameInstance.GainedSpeedUps = 0;
            GameController.GameInstance.GunHotDogAmount = 12;
            GameController.GameInstance.GunBurgerAmount = 12;


            if (GameController.GameInstance.gameDifficulty == "Easy") { GameController.GameInstance.gameTime = 180; GameController.GameInstance.numberOfCustomers = 8; }
            if (GameController.GameInstance.gameDifficulty == "Medium") { GameController.GameInstance.gameTime = 150; GameController.GameInstance.numberOfCustomers = 10; }
            if (GameController.GameInstance.gameDifficulty == "Hard") { GameController.GameInstance.gameTime = 120; GameController.GameInstance.numberOfCustomers = 12; }

            //GameController.GameInstance.gameTime = GameController.GameInstance.gameDifficulty == "Easy" ? 180 :
            //GameController.GameInstance.gameDifficulty == "Medium" ? 120 : 30;


            AlreadyEnded = false;


            //GameController.GameInstance.numberOfCustomers = GameController.GameInstance.gameDifficulty == "Easy" ? 8 :
            //GameController.GameInstance.gameDifficulty == "Medium" ? 120 : 10;
        }

        if (GameController.GameInstance.gameTime <= 0)
        {
            GameController.GameInstance.GainedSpeedUps = 0;
            GameController.GameInstance.playerSpeed = 10f;
            GameController.GameInstance.GunHotDogAmount = 12;
            GameController.GameInstance.GunBurgerAmount = 12;

            if (GameController.GameInstance.gameDifficulty == "Easy") { GameController.GameInstance.gameTime = 180; GameController.GameInstance.numberOfCustomers = 8; }
            if (GameController.GameInstance.gameDifficulty == "Medium") { GameController.GameInstance.gameTime = 150; GameController.GameInstance.numberOfCustomers = 10; }
            if (GameController.GameInstance.gameDifficulty == "Hard") { GameController.GameInstance.gameTime = 120; GameController.GameInstance.numberOfCustomers = 12; }



            //GameController.GameInstance.gameTime = GameController.GameInstance.gameDifficulty == "Easy" ? 180 :
            //GameController.GameInstance.gameDifficulty == "Medium" ? 120 : 30;


            AlreadyEnded = false;

            //GameController.GameInstance.numberOfCustomers = 1;
            totalTime = GameController.GameInstance.gameTime;
        }
    }
}
