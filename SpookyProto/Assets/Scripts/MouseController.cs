﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
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


        
    }
}