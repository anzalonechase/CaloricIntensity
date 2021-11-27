using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Bullet" && collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }

        if(gameObject.tag == "Bullet" && collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }


    }

}
