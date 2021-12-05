using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to allow the player to reset the level and access the pause menu and 
//instruction panel using the appropriate key inputs. 
public class LevelControl : MonoBehaviour
{
    private Scene thisScene;

    private GlobalControl globalController;

    public GameObject pausePanel;

    private float timerP;
    private bool canOpenClose;

    // Start is called before the first frame update
    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
            canOpenClose = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
        }
        if (globalController.pause) {
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene("Scene_Menu");
            }
        }
        if (Input.GetKey(KeyCode.P) && canOpenClose)
        {
            unpause();
        }
        if (!canOpenClose)
            {
                timerP += Time.deltaTime;
                if (timerP > .5)
                {
                    canOpenClose = true;
                    timerP = 0;
                }
            }     
    }
    public void unpause() {
        canOpenClose = false; 
            if(globalController.pause) {
                globalController.pause = false;
                pausePanel.SetActive(false);
            } else {
                globalController.pause = true;
                pausePanel.SetActive(true);
            }
    }
}

