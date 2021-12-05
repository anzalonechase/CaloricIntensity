using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to hold all the values that need to persist between scenes 
// and those that need to be access by various scripts. 
public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance {get; private set;}

    public bool musicState;
    public int musicTrack;
    public bool pause;


    void Awake ()   
       {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
       }

    
}
