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
    private Text score;
    [SerializeField] Sprite Soda;

    private bool AlreadyEnded;
    // Start is called before the first frame update





    private GameObject gameInformation;              // refrences the canvas to display the game information
    private bool informationActive;               // boolean to know how the user is toggling the panel

    private AudioSource onDrink;  //referneces drinking audio source FOUND IN EMPTY OBJECT UNDER CAMERA < AIM CONTROLLER 


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
        gameReset();
    }

    /**
     * Resetd the game for playing again
     */
    private void gameReset()
    {
        Time.timeScale = 1;
        occuring = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameController.GameInstance.GainedSpeedUps = 0;
        GameController.GameInstance.playerSpeed = 10f;
        if (GameController.GameInstance.itemList.Count > 2)
        {
            GameController.GameInstance.itemList.RemoveAt(GameController.GameInstance.itemList.Count - 1);
        }


        onDrink = GameObject.Find("OnDrink_Audio").GetComponent<AudioSource>();  //referneces audio clip with ondrink effect

        gameInformation = GameObject.Find("Canvas_Information"); // Get reference to the canvas gameobject
        informationActive = false;                        // Ste boolean to false because initially we dont want it shwoing

        gameInformation.SetActive(informationActive);         // Sets canvas to not visible
    }





    // Update is called once per frame
    void Update()
    {

        UpdateInventory();
        UpdateCustomer();
        GameWinnerFunctionality();
        GameOverConditionAndTimeFuctionality();
        OpenCloseHudeAndInventorySystem();

        
        




    }

    /**
     * Shows player name on the screen
     */
    private void updatePlayerName()
    {
        if (GameController.GameInstance.characterName != null)
        {
            PlayerName.text = "Welcome " + GameController.GameInstance.characterName;
        }
    }

    /**
     * If the player feeded require customers,
     * the game will end and winning message shows up
     * and its score will be calculated and shown on the screen
     */
    private void GameWinnerFunctionality()
    {
        if (GameController.GameInstance.numberOfCustomers <= 0)
        {
            if (!AlreadyEnded)
            {
                AlreadyEnded = true;
                Cursor.lockState = CursorLockMode.None;

                updateTopScorere();
                Time.timeScale = 0;
                WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
                
            }
        }
    }

    /**
     * Update Top score name, and score
     */
    private void updateTopScorere()
    {
        int playerScored = (100 * (int)totalTime);
        score.text = "Score: " + playerScored.ToString();
        if (GameController.GameInstance.gameDifficulty == "Easy")
        {
            if (playerScored > GameController.GameInstance.HighestScore[0])
            {
                GameController.GameInstance.HighestScore[0] = playerScored;

                GameController.GameInstance.topPlayer[0] = GameController.GameInstance.characterName;
            }
        }
        else if (GameController.GameInstance.gameDifficulty == "Medium")
        {
            if (playerScored > GameController.GameInstance.HighestScore[1])
            {
                GameController.GameInstance.HighestScore[1] = playerScored;

                GameController.GameInstance.topPlayer[1] = GameController.GameInstance.characterName;
            }
        }
        else if (GameController.GameInstance.gameDifficulty == "Hard")
        {
            if (playerScored > GameController.GameInstance.HighestScore[2])
            {
                GameController.GameInstance.HighestScore[2] = playerScored;

                GameController.GameInstance.topPlayer[2] = GameController.GameInstance.characterName;
            }
        }
    }

    /**
     *  Checking to see if game over
     *  If it is it shows game over screen
     *  stops the time,
     *  and let player decide to play again or not
     */
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

                onDrink.Play();
              
                StartCoroutine("speedupEffects");

            }

        }

        if (Input.GetKeyDown("i"))                             // Displays the information panel to the player if 'i' is pressed, if pressed again it will take it down
        {

            gameInformation.SetActive(!informationActive);            // Makes the panel appear and disappear according to the last toggle.
            informationActive = !informationActive;        // Changes the true and false according to the last toggle. This way it ensures that it will be taken down and up 
        }                                                  // as user pleases


    }

    /*
     * Handels the speedup soda effect functionality
     * Gives 3x speed to player for 20 seconds
     */
    private IEnumerator speedupEffects(){
        
        
        occuring = true;
        GameController.GameInstance.playerSpeed *= 2;
        yield return new WaitForSeconds(15f);
        GameController.GameInstance.playerSpeed /= 2;
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
    /**
     *  Initialising elements on game Over screen/Panel
     */
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
    /**
     *  Initialising elements on Winning screen/Panel
     */
    private void InitialiseWinningScreenAndButtons()
    {
     
        if(WinningScreen == null)
        {
            WinningScreen = GameObject.Find("WonPanel").gameObject;
            score = WinningScreen.transform.Find("Score").GetComponent<Text>();
            WinningScreenReplayBtn = WinningScreen.transform.Find("Replay").GetComponent<Button>();
            WinningScreenReplayBtn.onClick.AddListener(delegate { replayTheGame(gameOverPanel); });
            WinningScreenBackBtn = WinningScreen.transform.Find("Back").GetComponent<Button>();
            WinningScreenBackBtn.onClick.AddListener(delegate { LoadSceneByName("Scene_Menu"); });
            WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
        }
        
    }

    /**
     *  Initialising elements on HUD Panel
     */
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

    /**
     *  Initialising elements on enventory system screen/Panel
     */
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
 
    /**
     * It adds speedups to the enventory system
     */
    private void UpdateInventory()
    {
        if (GameController.GameInstance.GainedSpeedUps > 0 && GameController.GameInstance.GainedSpeedUps < 5)
        {
            for (int i = 0; i < GameController.GameInstance.itemList.Count; i++)
            {
                if (GameController.GameInstance.itemList[i].name == "Speedups")
                {
                    InventorySystemSpeedUpImage.enabled = true;
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
                InventorySystemSpeedUpImage.enabled = false;
            }
        }
        else
        {
            InventorySystemSpeedUpValue.text = GameController.GameInstance.GainedSpeedUps.ToString();
        }
        
    }

    /**
     * Doing appropriate reseting for replaying the game
     */
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

    /*
     * It loads the menu scene
     */
    public void LoadSceneByName(string sceneName)
    {
        GameController.GameInstance.GainedSpeedUps=0;
        InventorySystemSpeedUpValue.text = "";
        GameController.GameInstance.itemList.RemoveAt(GameController.GameInstance.itemList.Count - 1);
        InventorySystemSpeedUpImage.sprite = null;
        InventorySystemSpeedUpImage.enabled = false;
        SceneManager.LoadScene(sceneName);
    }

    /*
    * It reset the game for a new game comming from character selection screen
    */
    private void startNewGameFunctionality()
    {
        
        if (GameController.GameInstance.numberOfCustomers <= 0)
        {
            GameController.GameInstance.playerSpeed = 10f;
            GameController.GameInstance.GainedSpeedUps = 0;

            if (GameController.GameInstance.gameDifficulty == "Easy") { GameController.GameInstance.gameTime = 240; GameController.GameInstance.numberOfCustomers = 8; }
            if (GameController.GameInstance.gameDifficulty == "Medium") { GameController.GameInstance.gameTime = 210; GameController.GameInstance.numberOfCustomers = 10; }
            if (GameController.GameInstance.gameDifficulty == "Hard") { GameController.GameInstance.gameTime = 180; GameController.GameInstance.numberOfCustomers = 12; }



            AlreadyEnded = false;


        }

        if (GameController.GameInstance.gameTime <= 0)
        {
            GameController.GameInstance.GainedSpeedUps = 0;
            GameController.GameInstance.playerSpeed = 10f;

            if (GameController.GameInstance.gameDifficulty == "Easy") { GameController.GameInstance.gameTime = 240; GameController.GameInstance.numberOfCustomers = 8; }
            if (GameController.GameInstance.gameDifficulty == "Medium") { GameController.GameInstance.gameTime = 210; GameController.GameInstance.numberOfCustomers = 10; }
            if (GameController.GameInstance.gameDifficulty == "Hard") { GameController.GameInstance.gameTime = 180; GameController.GameInstance.numberOfCustomers = 12; }


            AlreadyEnded = false;

            totalTime = GameController.GameInstance.gameTime;
        }

        GameController.GameInstance.GunHotDogAmount = 12;
        GameController.GameInstance.GunBurgerAmount = 12;
    }
}
