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
        if(gameObject.tag == "Bullet" && collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }

        if(gameObject.tag == "Bullet" && collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }


    }

}
