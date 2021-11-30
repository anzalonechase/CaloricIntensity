using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class HudHandler : MonoBehaviour
{
    private GameObject Timer;
    private GameObject Customers;
    private GameObject HUDHolder;
    private Image BurgerHolder;
    private Image hotDogHolder;
    private Text RemainingTime;
    private Text RemainingCustomers;
    private int minute;
    private int seconds;
    private float totalTime;
    private int totalCustomers;
    private Image[] burgers;
    private Image[] hotDogs;
    // Start is called before the first frame update
    private void Awake()
    {
        Timer = GameObject.Find("Timer").gameObject;
        Customers = GameObject.Find("Customers").gameObject;
        RemainingTime = Timer.transform.Find("RemainingTime").GetComponent<Text>();
        RemainingCustomers = Customers.transform.Find("RemainingCustomers").GetComponent<Text>();
        BurgerHolder = GameObject.Find("BurgerHolder").GetComponent<Image>();
        hotDogHolder = GameObject.Find("HotDogHolder").GetComponent<Image>();
        burgers = new Image[6];
        hotDogs = new Image[6];
        totalTime = 180;
        HUDHolder = this.gameObject;

        CalculateRemainingTime();
        
    }

   
    void Start()
    {

        StartCoroutine("updateFood");
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime >= 0)
        {
            totalTime -= Time.deltaTime;
            CalculateRemainingTime();
        }
        if (totalCustomers >= 0)
        {
            UpdateCustomer();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            HUDHolder.gameObject.SetActive(!HUDHolder.gameObject.activeInHierarchy);
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
        
            totalCustomers = (int)(totalTime / 6) * 10;
            RemainingCustomers.text = "Remaining Customers: " + (totalCustomers).ToString();
        
        
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



}
