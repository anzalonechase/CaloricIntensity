using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_Controller : MonoBehaviour
{
    public float moveSpeed;   //----------------------            might delete all this later when i find proper movement

    public Rigidbody rb;     //------------------------

    public float flipBy;  //------------------------

    public SpriteRenderer burgerSprite; //the sprite of the burger
    public SpriteRenderer hotdogSprite; //the sprite of the hot dog
    public SpriteRenderer speechSprite; //the sprite of the speech bubble






    public TextMeshPro  order;  // references teh text above NPC  

    public int numHotdogs = 0;     // How many hotdogs will the npc want 
    public int numBurgers = 0;    // How many burgers will the npc want 




    public Transform playerPos;         // This is to get a reference to the player transform
    public Transform textBox;      // This is to get a reference to the textbox transform 
                                   // This will be used to make the textbook LookAt() the player that way 
                                   // the textbox is always looking at player (Makes it easy to read)







    void Start()
    {
        // I have to use the inspector for these bc NPC will be a prefab

        //rb = GameObject.Find("NPC").GetComponent<Rigidbody>();              //-----------------
        //playerPos = GameObject.Find("Player").GetComponent<Transform>();                    // refernece the player transform
        //textBox = GameObject.Find("Order_Correct_Orientation").GetComponent<Transform>();    // reference the textbox transform        (To make sure textbox is LookAt() player
        //order = GameObject.Find("Order_Text").GetComponent<TextMeshPro>();                  // Gets reference to the text above NPC



        while(numBurgers == 0 & numBurgers == 0)   // This makes sure that both the hotdog and burger count are never both 0
        {
            numHotdogs = Random.Range(0, 4);  // 0, 1, 2, or 3
            numBurgers = Random.Range(0, 4);

        }

        order.text = (numBurgers + ":       \n" + numHotdogs + ":       ");       // Puts out the order to NPC text box













        //moveSpeed = 400;          //------------------------
        //       flipBy = 270;    //On first flip we change the rotation of y to 270 which is exactly opposite way   //----------------------------
    }

    
    void Update()
    {


        //     rb.velocity = transform.forward * Time.deltaTime * moveSpeed;         // Makes it move         ------------------------






        textBox.LookAt(playerPos);






        if (numHotdogs == 0 & numBurgers == 0)              // IF the numeber of hotdogs adn burgers are both 0 then that means the order has been statisfied
        {
            order.text = "    THANKS!";  // extra spaces for buffering
            burgerSprite.enabled = false;  //disable the burger and hotdog sprites
            hotdogSprite.enabled = false;
            speechSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1);  //resize the speech bubble
        }
        else                             // Else order has not been completely satisfied and it should display what is left of the order
        {
            order.text = (numBurgers + ":    \n" + numHotdogs + ":     ");       // Puts out the order to NPC text box REMAINING
        }






    }

    public void OnCollisionEnter(Collision collision)
    {




        if(gameObject.tag == "NPC" && collision.gameObject.tag =="Boundary" )                              // -----------
        { 
          
            transform.rotation = Quaternion.Euler(new Vector3(0,flipBy,0));  // Flips npc by 180 each time

            flipBy += 180;   // TO make sure that next time the rotation is set, it will go exactly the opposite way

        }









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
