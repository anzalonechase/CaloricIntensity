using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Controller : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody rb;

    public float flipBy;






    public Text txt;                  // To display box above NPC              // (Both set in inspector)

    public Transform positionText;  // WIll reference where the position of the text should be



    

    void Start()
    {
        rb =GameObject.Find("NPC").GetComponent<Rigidbody>();

        moveSpeed = 400;

        flipBy = 270;    //On first flip we change the rotation of y to 270 which is exactly opposite way
    }

    
    void Update()
    {





        Vector3 namePos = Camera.main.WorldToScreenPoint(positionText.position);         // Puts text right above NPC head
        txt.transform.position = namePos;






        rb.velocity = transform.forward * Time.deltaTime * moveSpeed;         // Makes it move 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "NPC" && collision.gameObject.tag =="Boundary" )
        {
          
            transform.rotation = Quaternion.Euler(new Vector3(0,flipBy,0));  // Flips npc by 180 each time

            flipBy += 180;   // TO make sure that next time the rotation is set, it will go exactly the opposite way

        }
    }
}
