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
        totalTime = 180;

        CalculateRemainingTime();
        
    }

   
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        CalculateRemainingTime();
        UpdateCustomer();
        //initialiseImageArray();
    }

    private void CalculateRemainingTime()
    {
        minute = (int)totalTime / 60;
        seconds = (int)totalTime % 60;
        RemainingTime.text = "Remaining Time: " + minute + ":" + seconds;
    }
    private void UpdateCustomer()
    {
        totalCustomers = (int)(totalTime / 6) * 10;
        RemainingCustomers.text = "Remaining Customers: "+(totalCustomers).ToString();
    }

    

}
