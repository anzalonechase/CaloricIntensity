using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDog_Bullet_Controller : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        if(gameObject.tag == "Hotdog" && collision.gameObject.tag == "Terrain")  // If there is a collision withthis tag the hotdog will destroy itself after some time
        {
            
            Destroy(gameObject, 2f);
        }

        if(gameObject.tag == "Hotdog" && collision.gameObject.tag == "Obstacle")  // If there is a collision with this tag the hotdog will destroy itself after some time
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Soda")   // If there is a collision with this tag the burger will destroy itself instantly
        {
            bool exists = false;
            for (int i = 0; i < GameController.GameInstance.itemList.Count; i++)
            {
                if (GameController.GameInstance.itemList[i].name == "Speedups")
                {
                    GameController.GameInstance.itemList[i].count++;
                    exists = true;

                }
            }
            if (!exists)
            {
                GameController.GameInstance.itemList.Add(new InventoryItem("Speedups", 1));
            }

            GameController.GameInstance.GainedSpeedUps++;
            Destroy(collision.gameObject, 0.5f);
            Destroy(gameObject);
        }


        // The NPC Controller will ahndle a collisison bewteen hotdog adn the npc

    }

}
