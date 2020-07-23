using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private bool isCrouching = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //our x grid rotation subtracts the lower our mouse goes on the y axis
        xRotation -= MouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Controls the movement of the x graph
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Controls the y movement
        playerTransform.Rotate(Vector3.up * MouseX);

        //Crouching
        if(Input.GetKeyDown(KeyCode.LeftShift) && isCrouching == false)
        {
            transform.position -= new Vector3(0f, .5f, 0f);
            isCrouching = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift) && isCrouching == true)
        {
            transform.position += new Vector3(0f, .5f, 0f);
            isCrouching = false;
        }
        
    }

   
}
