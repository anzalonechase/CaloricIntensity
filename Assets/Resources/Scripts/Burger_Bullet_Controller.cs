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
        if (gameObject.tag == "Bullet" && collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }

        if (gameObject.tag == "Bullet" && collision.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }


    }

}
