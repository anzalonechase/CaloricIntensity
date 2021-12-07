using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeaderBoardHandeler : MonoBehaviour
{
    private Text topPlayerEasy;
    private Text topPlayerMedium;
    private Text topPlayerHard;
    private Text topPlayerEasyScore;
    private Text topPlayerMediumScpre;
    private Text topPlayerHardScore;
    private Button back;
    // Start is called before the first frame update
    void Start()
    {
        topPlayerEasy = gameObject.transform.Find("EasyTopPlayer").GetComponent<Text>();
        topPlayerMedium = gameObject.transform.Find("MediumTopPlayer").GetComponent<Text>();
        topPlayerHard = gameObject.transform.Find("HardTopPlayer").GetComponent<Text>();

        topPlayerEasyScore = gameObject.transform.Find("EasyTopPlayerScore").GetComponent<Text>();
        topPlayerMediumScpre = gameObject.transform.Find("MediumTopPlayerScore").GetComponent<Text>();
        topPlayerHardScore = gameObject.transform.Find("HardTopPlayerScore").GetComponent<Text>();
        back = gameObject.transform.Find("Back").GetComponent<Button>();
        back.onClick.AddListener(delegate { SceneManager.LoadScene("Scene_Menu"); });
    }

    // Update is called once per frame
    void Update()
    {
        topPlayerEasyScore.text = GameController.GameInstance.HighestScore[0].ToString();
        if (GameController.GameInstance.HighestScore[0] > 0)
        {
            topPlayerEasy.text = GameController.GameInstance.topPlayer[0];
        }
        topPlayerMediumScpre.text = GameController.GameInstance.HighestScore[1].ToString();
        if (GameController.GameInstance.HighestScore[1] > 0)
        {
            topPlayerMedium.text = GameController.GameInstance.topPlayer[1];
        }
        topPlayerHardScore.text = GameController.GameInstance.HighestScore[2].ToString();
        if (GameController.GameInstance.HighestScore[2] > 0)
        {
            topPlayerHardScore.text = GameController.GameInstance.topPlayer[2];
        }
    }
}
