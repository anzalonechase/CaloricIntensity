using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script is designed to load scenes for whatever buttons may be trying to do so
public class SceneLoader : MonoBehaviour
{

    private Scene thisScene;
    private GlobalControl globalController;

    void Start()
    {
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("Scene_Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Scene_Chase");
    }

    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene("Scene_CharacterSelection");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Scene_Settings");
    }

    public void LoadAbout()
    {
        SceneManager.LoadScene("Scene_About");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Scene_Credits");
    }


    //When the Exit button is clicked, quit the application.
    public void doExitGame()
    {
#if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;   
#else  
          Application.Quit();
#endif
    }
}
