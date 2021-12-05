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
    private GameObject InventorySystem;
    private GameObject gameOverPanel;
    private GameObject WinningScreen;
    private Button gameOverReplayBtn;
    private Button gameOverBackBtn;
    private Button WinningScreenReplayBtn;
    private Button WinningScreenBackBtn;

    private Text RemainingTime;
    private Text RemainingCustomers;
    private int minute;
    private int seconds;
    private float totalTime;
   
    private bool AlreadyEnded;
    // Start is called before the first frame update
    private void Awake()
    {

        InitialiseGameOverScreenAndButtons();
        InitialiseWinningScreenAndButtons();

        InitialiseHUDTextAndButtons();
        CalculateRemainingTime();
     //   WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);



      //  gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeInHierarchy);

    }

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        AlreadyEnded = false;
        if(GameController.GameInstance.numberOfCustomers==0)
            GameController.GameInstance.numberOfCustomers = 1;



        
        GameController.GameInstance.gameTime = GameController.GameInstance.gameDifficulty=="Easy"?180: 
            GameController.GameInstance.gameDifficulty == "Medium"?120:60;
        totalTime = GameController.GameInstance.gameTime;
        // StartCoroutine("updateFood");
        

    }

    // Update is called once per frame
    void Update()
    {
        //GameController.GameInstance.itemList.Add(new InventoryItem("Burger", 12));
        //GameController.GameInstance.itemList.Add(new InventoryItem("HotDog", 12));
        UpdateCustomer();
        GameWinnerFunctionality();
        GameOverConditionAndTimeFuctionality();
        OpenCloseHudeAndInventorySystem();
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
    private void OpenCloseHudeAndInventorySystem()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HUDHolder.gameObject.SetActive(!HUDHolder.gameObject.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventorySystem.gameObject.SetActive(!InventorySystem.gameObject.activeInHierarchy);
        }
    }
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
    private void UpdateCustomer()
    {
       
        RemainingCustomers.text = "Remaining Customers: " + (GameController.GameInstance.numberOfCustomers).ToString();
        
        
    }

   
    

    /*private IEnumerator updateFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            initialiseImageArray();
            
        }

    }*/

   

    private void InitialiseGameOverScreenAndButtons()
    {
        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel").gameObject;
            gameOverReplayBtn = gameOverPanel.transform.Find("Replay").GetComponent<Button>();
            gameOverBackBtn = gameOverPanel.transform.Find("Back").GetComponent<Button>();
            // gameOverBackBtn.onClick.AddListener(delegate { LoadSceneByNumber(0); });
            gameOverReplayBtn.onClick.AddListener(delegate { replayTheGame(gameOverPanel); });
            gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeInHierarchy);
        }
        
    }

    private void InitialiseWinningScreenAndButtons()
    {
     
        if(WinningScreen== null)
        {
            WinningScreen = GameObject.Find("WonPanel").gameObject;
            WinningScreenBackBtn = WinningScreen.transform.Find("Back").GetComponent<Button>();
            WinningScreenReplayBtn = WinningScreen.transform.Find("Replay").GetComponent<Button>();
            // WinningScreenBackBtn.onClick.AddListener(delegate { LoadSceneByNumber(0);});
            WinningScreenReplayBtn.onClick.AddListener(delegate { replayTheGame(WinningScreen); });
            WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
        }
        
    }
    private void InitialiseHUDTextAndButtons()
    {

        HUDHolder = GameObject.Find("HUDPanel").gameObject;
        Timer = HUDHolder.transform.Find("Timer").gameObject;
        Customers = HUDHolder.transform.Find("Customers").gameObject;
        RemainingTime = Timer.transform.Find("RemainingTime").GetComponent<Text>();
        RemainingCustomers = Customers.transform.Find("RemainingCustomers").GetComponent<Text>();
      
        InventorySystem = GameObject.Find("GameInventoryPanel").gameObject;
     
    }
    private void replayTheGame(GameObject panel)
    {
        panel.gameObject.SetActive(!panel.gameObject.activeInHierarchy);
        SceneManager.LoadScene("Scene_Chase");
        
    }

    public void LoadSceneByNumber(int sceneNumber)
    {

        SceneManager.LoadScene(sceneNumber);
    }
}
