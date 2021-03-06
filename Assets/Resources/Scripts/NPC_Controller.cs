using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_Controller : MonoBehaviour
{
    

    public SpriteRenderer burgerSprite; //the sprite of the burger
    public SpriteRenderer hotdogSprite; //the sprite of the hot dog
    public SpriteRenderer speechSprite; //the sprite of the speech bubble
    private AudioSource onFedAudio; // "YAY" audio clip for customer satisfaction condition






    public TextMeshPro  order;  // references teh text above NPC  

    public int numHotdogs = 0;     // How many hotdogs will the npc want 
    public int numBurgers = 0;    // How many burgers will the npc want 




    public Transform playerPos;         // This is to get a reference to the player transform
    public Transform textBox;      // This is to get a reference to the textbox transform 
                                   // This will be used to make the textbook LookAt() the player that way 
                                   // the textbox is always looking at player (Makes it easy to read)




    private bool customerSatisfied = false;


    void Start()
    {

        while (numBurgers == 0 & numBurgers == 0)   // This makes sure that both the hotdog and burger count are never both 0
        {
            numHotdogs = Random.Range(0, 4);  // 0, 1, 2, or 3
            numBurgers = Random.Range(0, 4);

        }

        order.text = (numBurgers + ":       \n" + numHotdogs + ":       ");       // Puts out the order to NPC text box
        onFedAudio = GameObject.Find("onFedAudio").GetComponent<AudioSource>(); // "Yay" sound being linked

    }

    
    void Update()
    {


     
        textBox.LookAt(playerPos);






        if (numHotdogs == 0 & numBurgers == 0)              // IF the numeber of hotdogs adn burgers are both 0 then that means the order has been statisfied
        {
            if (customerSatisfied == false)
            {
                order.text = "    THANKS!";  // extra spaces for buffering
                customerSatisfied = true;
                onFedAudio.Play(); // "Yay" sound invoked at condition
                GameController.GameInstance.numberOfCustomers--;
                burgerSprite.enabled = false;  //disable the burger and hotdog sprites
                hotdogSprite.enabled = false;
            }
            
            speechSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1);  //resize the speech bubble
        }
        else                             // Else order has not been completely satisfied and it should display what is left of the order
        {
            order.text = (numBurgers + ":    \n" + numHotdogs + ":     ");       // Puts out the order to NPC text box REMAINING
        }






    }

    public void OnCollisionEnter(Collision collision)
    {

        if (gameObject.tag == "NPC" && collision.gameObject.tag == "Burger")          // If hit by a burger 
        {

            if (numBurgers != 0)       // Then its still a positive number
            {
                numBurgers -= 1;  // Decrease order for burgers

                Destroy(collision.gameObject);       // If the burger is accepted then destroy it otherwise dont destroy it and let it hit the ground
            }
            else 
            {
            // Do something because the player just gave an extra burger 
            }

        }


        if (gameObject.tag == "NPC" && collision.gameObject.tag == "Hotdog")          // If hit by a hotdog 
        {

            if (numHotdogs != 0)       // Then its still a positive number
            {
                numHotdogs -= 1;  // Decrease order for htodogs

                Destroy(collision.gameObject);       // If the hotdog is accepted then destroy it otherwise dont destroy it and let it hit the ground
            }
            else
            {
                // Do something because the player just gave an extra hotdog 
            }
        }





    }
}
