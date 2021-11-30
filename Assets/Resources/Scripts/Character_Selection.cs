using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Character_Selection : MonoBehaviour
{
    private Button backBtn;
    private Button quitBtn;
    // Start is called before the first frame update
    void Start()
    {
        initilisedBackButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initilisedBackButton()
    {
        backBtn = GameObject.Find("BackBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(delegate { LoadSceneByNumber(0); });

        quitBtn = GameObject.Find("QuitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(delegate { exitFromtheEditor(); });
    }
    private void LoadSceneByNumber(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    /* 
    * When user click the exit button. it exit from editor, and play mode
    */
    private void exitFromtheEditor()
    {
        // save any game data here
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                         Application.Quit();
                         Application.Quit();
        #endif
    }

}
