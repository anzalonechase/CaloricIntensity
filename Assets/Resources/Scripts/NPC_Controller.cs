using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_Controller : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody rb;

    public float flipBy;






    public TextMeshPro  order;  // references teh text above NPC  

    public int numHotdogs = 0;     // How many hotdogs will the npc want 
    public int numBurgers = 0;    // How many burgers will the npc want 












    void Start()
    {
        rb = GameObject.Find("NPC").GetComponent<Rigidbody>();
         









        order = GameObject.Find("Order").GetComponent<TextMeshPro>();   // Gets reference to the text above NPC

        while(numBurgers == 0 & numBurgers == 0)   // This makes sure that both the hotdog and burger count are never both 0
        {
            numHotdogs = Random.Range(0, 4);  // 0, 1, 2, or 3
            numBurgers = Random.Range(0, 4);

        }

        order.text = (numBurgers + "  Burgers\n" + numHotdogs + "  Hotdogs");       // Puts out the order to NPC text box













        moveSpeed = 400;

        flipBy = 270;    //On first flip we change the rotation of y to 270 which is exactly opposite way
    }

    
    void Update()
    {
        rb.velocity = transform.forward * Time.deltaTime * moveSpeed;         // Makes it move 








        if (numHotdogs == 0 & numBurgers == 0)              // IF the numeber of hotdogs adn burgers are both 0 then that means the order has been statisfied
        {
            order.text = "THANK YOU";
        }
        else                             // Else order has not been completely satisfied and it should display what is left of the order
        {
            order.text = (numBurgers + "  Burgers\n" + numHotdogs + "  Hotdogs");       // Puts out the order to NPC text box REMAINING
        }






    }

    public void OnCollisionEnter(Collision collision)
    {

        if(gameObject.tag == "NPC" && collision.gameObject.tag =="Boundary" )
        {
          
            transform.rotation = Quaternion.Euler(new Vector3(0,flipBy,0));  // Flips npc by 180 each time

            flipBy += 180;   // TO make sure that next time the rotation is set, it will go exactly the opposite way

        }










        if (gameObject.tag == "NPC" && collision.gameObject.tag == "Burger")          // If hit by a burger decrease th burger order
        {
            if (numBurgers != 0)       // Then its still a positive number
            {
                numBurgers -= 1;  // Decrease order for burgers
            }
            else 
            {
             // Do something because the player just gave an extra burger 
            }
        }

        if (gameObject.tag == "NPC" && collision.gameObject.tag == "Hotdog")          // If hit by a hotdog decrease the hotdog order
        {
            if (numHotdogs != 0)       // Then its still a positive number
            {
                numHotdogs -= 1;  // Decrease order for htodogs
            }
            else
            {
                // Do something because the player just gave an extra hotdog 
            }
        }





    }
}
