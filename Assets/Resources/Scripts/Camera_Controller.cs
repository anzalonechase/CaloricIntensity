using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

    public float mouseSense = 100f;           // mosue sensitivty
    public float xRotation = 0f;        // rotation of chcarcter

    public Transform playerBody;       //will need to actually rotate character       (Set in inspector)



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;  
        float mouseY = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
