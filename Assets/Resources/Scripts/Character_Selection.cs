using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Character_Selection : MonoBehaviour
{
    private Button backBtn;
    private Button quitBtn;
    private InputField characterName;
    private Dropdown gameDifficultyDropDownMenu;
    private Dictionary<string, string> gameDifficultyHolder;
    private Text gameDifficultyDesc;




    // Start is called before the first frame update
    void Start()
    {
        initiliseBackAndQuitButton();
        initiliseCharacterNameInputField();
        initilisegameDifficultyDropDownMenu();
    }

    private void initilisegameDifficultyDropDownMenu()
    {
        gameDifficultyDropDownMenu = GameObject.Find("GameDifficultyDropdown").GetComponent<Dropdown>();
        GameObject gameDifficultylabel = gameDifficultyDropDownMenu.transform.Find("Label").GetComponent<GameObject>();
        


        gameDifficultyDropDownMenu.options.Clear();
        gameDifficultyHolder = new Dictionary<string, string>();
        gameDifficultyHolder.Add("Easy", "You'll have 180 seconds to complete the game.");
        gameDifficultyHolder.Add("Medium", "You'll have 120 seconds to complete the game.");
        gameDifficultyHolder.Add("Hard", "You'll have 60 seconds to complete the game.");
        gameDifficultyDesc = GameObject.Find("DropDownMenuDesc").GetComponent<Text>();

        foreach (var DifficultyLevel in gameDifficultyHolder)
        {
            gameDifficultyDropDownMenu.options.Add(new Dropdown.OptionData() { text = DifficultyLevel.Key });

        }
        gameDifficultyDropDownMenu.onValueChanged.AddListener(delegate { SelectedItem(); });

    }

    private void SelectedItem()
    {
        int index = gameDifficultyDropDownMenu.value;
        string selectedOption = gameDifficultyDropDownMenu.options[index].text;
        foreach (var DifficultyLevel in gameDifficultyHolder)
        {
            if (DifficultyLevel.Key == selectedOption)
            {
                gameDifficultyDesc.text = DifficultyLevel.Value;
            }
        }

        GameController.GameInstance.gameDifficulty = selectedOption;
        if (GameController.GameInstance.gameDifficulty == "Easy")
        {
            GameController.GameInstance.gameTime = 180;
        }
        else if (GameController.GameInstance.gameDifficulty == "Medium")
        {
            GameController.GameInstance.gameTime = 120;
        }
        else if (GameController.GameInstance.gameDifficulty == "Hard")
        {
            GameController.GameInstance.gameTime = 60;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void initiliseCharacterNameInputField()
    {
        characterName = GameObject.Find("CharacterName").GetComponent<InputField>();
        characterName.onValueChanged.AddListener(delegate { setName(); });

    }

    private void setName()
    {
            GameController.GameInstance.characterName = characterName.text;
            Debug.Log(GameController.GameInstance.characterName);
    }

    private void initiliseBackAndQuitButton()
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
