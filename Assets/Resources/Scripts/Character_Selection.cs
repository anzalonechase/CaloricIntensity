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
    private Button makeCharacterBtn;
    private InputField characterName;
    private Dropdown gameDifficultyDropDownMenu;
    private Dictionary<string, string> gameDifficultyHolder;
    private Text gameDifficultyDesc;
    private Slider redSlider;
    private Slider blueSlider;
    private Slider greenSlider;
    private Image characterColor;
    private GameObject colors;




    // Start is called before the first frame update
    void Start()
    {
        
        initiliseButtons();
        initiliseCharacterNameInputField();
        initilisegameDifficultyDropDownMenu();
        initialiseSliders();
        ReseGameContoller();
    }
    private void ReseGameContoller()
    {
        GameController.GameInstance.gameDifficulty = "Easy";
        GameController.GameInstance.gameTime = 240;
        GameController.GameInstance.HUDColor = new Color32(0, 0, 0, 255);
        GameController.GameInstance.redColor = 0;
        GameController.GameInstance.blueColor = 0;
        GameController.GameInstance.greenColor = 0;
        GameController.GameInstance.GainedSpeedUps = 0;
        GameController.GameInstance.characterName = "";
        characterColor.color = new Color32(0, 0, 0, 255);
    }
    private void initialiseSliders()
    {
        colors = GameObject.Find("CharacterColor").gameObject;
        redSlider = colors.transform.Find("RedColor").GetComponent<Slider>();
        greenSlider = colors.transform.Find("GreenColor").GetComponent<Slider>();
        blueSlider = colors.transform.Find("BlueColor").GetComponent<Slider>();
        Image playerColor = colors.transform.Find("CharacterColor").GetComponent<Image>();
        characterColor = playerColor.transform.GetComponent<Image>();

        redSlider.onValueChanged.AddListener(delegate { onChangeRedSliderValue(); });
        greenSlider.onValueChanged.AddListener(delegate { onChangeGreenSliderValue(); });
        blueSlider.onValueChanged.AddListener(delegate { onChangeBlueSliderValue(); });


    }

    public void onChangeRedSliderValue()
    {
        GameController.GameInstance.redColor = (byte)redSlider.value;
        changeImageColor();
    }

    public void onChangeGreenSliderValue()
    {
        GameController.GameInstance.greenColor = (byte)greenSlider.value;
        changeImageColor();
    }

    public void onChangeBlueSliderValue()
    {
        GameController.GameInstance.blueColor = (byte)blueSlider.value;
        changeImageColor();
    }

    private void changeImageColor()
    {
        GameController.GameInstance.HUDColor = new Color32(GameController.GameInstance.redColor,
        GameController.GameInstance.greenColor, GameController.GameInstance.blueColor, 255);
        characterColor.color = new Color32(GameController.GameInstance.redColor,
        GameController.GameInstance.greenColor, GameController.GameInstance.blueColor, 255);
    }


    private void initilisegameDifficultyDropDownMenu()
    {
        gameDifficultyDropDownMenu = GameObject.Find("GameDifficultyDropdown").GetComponent<Dropdown>();

        gameDifficultyDropDownMenu.options.Clear();
        gameDifficultyHolder = new Dictionary<string, string>();
        gameDifficultyHolder.Add("Easy", "To win, you'll have 240 seconds to feed 8 customers.");
        gameDifficultyHolder.Add("Medium", "To win, you'll have 210 seconds to feed 10 customers.");
        gameDifficultyHolder.Add("Hard", "To win, you'll have 180 seconds to feed 12 customers.");
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
            GameController.GameInstance.gameTime = 240;
            GameController.GameInstance.numberOfCustomers = 8;
        }
        else if (GameController.GameInstance.gameDifficulty == "Medium")
        {
            GameController.GameInstance.gameTime = 210;
            GameController.GameInstance.numberOfCustomers = 10;
        }
        else if (GameController.GameInstance.gameDifficulty == "Hard")
        {
            GameController.GameInstance.gameTime = 180;
            GameController.GameInstance.numberOfCustomers = 12;
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
    }

    private void initiliseButtons()
    {
        backBtn = GameObject.Find("BackBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(delegate { LoadSceneByNumber(0); });

        quitBtn = GameObject.Find("QuitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(delegate { exitFromtheEditor(); });

        makeCharacterBtn = GameObject.Find("MakeCharacter").GetComponent<Button>();
        makeCharacterBtn.onClick.AddListener(delegate { 
            submitCharacter();
            LoadSceneByNumber(3); 
        });
    }

    /**
     * Creating the character and setting created to true
     */
    private void submitCharacter()
    {
        setName();

        GameController.GameInstance.itemList.Add(new InventoryItem("Burger", 12));
        GameController.GameInstance.itemList.Add(new InventoryItem("HotDog", 12));
        GameController.GameInstance.created = true;
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
