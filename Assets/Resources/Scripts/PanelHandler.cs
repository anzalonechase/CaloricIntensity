using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Image BurgerHolder;
    private Image hotDogHolder;
    private Text RemainingTime;
    private Text RemainingCustomers;
    private int minute;
    private int seconds;
    private float totalTime;
    private Image[] burgers;
    private Image[] hotDogs;
    private bool AlreadyEnded;
    // Start is called before the first frame update
    private void Awake()
    {
        gameOverPanel = GameObject.Find("GameOverPanel").gameObject;
        gameOverReplayBtn = gameOverPanel.transform.Find("Replay").GetComponent<Button>();
        gameOverBackBtn = gameOverPanel.transform.Find("Back").GetComponent<Button>();
        gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeInHierarchy);
        WinningScreen = GameObject.Find("WonPanel").gameObject;
        WinningScreenBackBtn = WinningScreen.transform.Find("Back").GetComponent<Button>();
        WinningScreenReplayBtn = WinningScreen.transform.Find("Replay").GetComponent<Button>();
        WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
        HUDHolder = GameObject.Find("HUDPanel").gameObject;
        Timer = HUDHolder.transform.Find("Timer").gameObject;
        Customers = HUDHolder.transform.Find("Customers").gameObject;
        RemainingTime = Timer.transform.Find("RemainingTime").GetComponent<Text>();
        RemainingCustomers = Customers.transform.Find("RemainingCustomers").GetComponent<Text>();
        BurgerHolder = HUDHolder.transform.Find("BurgerHolder").GetComponent<Image>();
        hotDogHolder = HUDHolder.transform.Find("HotDogHolder").GetComponent<Image>();
        InventorySystem = GameObject.Find("GameInventoryPanel").gameObject;
        burgers = new Image[6];
        hotDogs = new Image[6];


        CalculateRemainingTime();

        AlreadyEnded = false;

    }

   
    void Start()
    {
        totalTime = GameController.GameInstance.gameTime;
        StartCoroutine("updateFood");
        StartCoroutine("updateFoodCoroutine");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.GameInstance.numberOfCustomers == 0)
        {
            if (!AlreadyEnded)
            {
                AlreadyEnded = true;
                Time.timeScale = 0;
                WinningScreen.gameObject.SetActive(!WinningScreen.gameObject.activeInHierarchy);
            }
        }
        if (totalTime >= 0)
        {
            totalTime -= Time.deltaTime;
            CalculateRemainingTime();
        }
        else
        {
            if (!AlreadyEnded)
            {
                AlreadyEnded = true;
                Time.timeScale = 0;
                gameOverPanel.gameObject.SetActive(!gameOverPanel.gameObject.activeInHierarchy);
            }
            
        }

        OpenCloseHudeAndInventorySystem();
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
        GameController.GameInstance.numberOfCustomers--;
        RemainingCustomers.text = "Remaining Customers: " + (GameController.GameInstance.numberOfCustomers).ToString();
        
        
    }

    private void initialiseImageArray()
    {

        int randomNum = Random.Range(1, burgers.Length + 1);
        
        for (int i = 1; i < burgers.Length + 1; i++)
        {

            burgers[i - 1] = BurgerHolder.transform.Find((i).ToString()).GetComponent<Image>();
            hotDogs[i - 1] = hotDogHolder.transform.Find((i).ToString()).GetComponent<Image>();
            if (i < randomNum)
            {
                burgers[i - 1].enabled = false;
                hotDogs[i - 1].enabled = false;
            }
            else
            {
                if(burgers[i - 1].enabled == false)
                {
                    burgers[i - 1].enabled = true;
                }
                if (hotDogs[i - 1].enabled == false)
                {
                    hotDogs[i - 1].enabled = true;
                }
            }


        }

    }

    private IEnumerator updateFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            initialiseImageArray();
            
        }

    }

    private IEnumerator updateFoodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            UpdateCustomer();

        }

    }



}
