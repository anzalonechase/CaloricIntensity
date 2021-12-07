using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the music track they are listening to
// and mute the music if so desired, all from the Settings scene.
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    private Button buttonMute;
    private Text muteButtonText;
    private GlobalControl globalController;
    public AudioClip Track1;

    void Start () {
        audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        buttonMute = GameObject.Find("MuteButton").GetComponent<Button>();
        muteButtonText = GameObject.Find("MuteText").GetComponent<Text>();
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>(); 
        buttonMute.onClick.AddListener( () => {ChangeMusicState(); }  );
    }
    
    void ChangeMusicState() {
        if (globalController.musicState) {
            muteButtonText.text = "Enable Music";
        } else {
            muteButtonText.text = "Disable Music";
        }
        globalController.musicState = !globalController.musicState;
        audioSource.mute = !audioSource.mute;
    }
}
