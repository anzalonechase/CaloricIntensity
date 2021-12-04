using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger_Bullet_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        if (gameObject.tag == "Burger" && collision.gameObject.tag == "Terrain")   // If there is a collision with this tag the burger will destroy itself after some time
        {
            Destroy(gameObject, 2f);
        }

        if (gameObject.tag == "Burger" && collision.gameObject.tag == "Obstacle")   // If there is a collision with this tag the burger will destroy itself instantly
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Soda" )   // If there is a collision with this tag the burger will destroy itself instantly
        {
            Destroy(collision.gameObject,0.5f);
        }




        // The NPC Controller will ahndle a collisison bewteen hotdog adn the npc

    }

}
