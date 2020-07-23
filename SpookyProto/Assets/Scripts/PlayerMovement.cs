﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public List<GameObject> toShrink = new List<GameObject>();
    public float moveSpeed;
    private float forwardInput;
    private float sidewaysInput;
    private Vector3 Crouch = new Vector3(0f, 0.5f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical") * moveSpeed;
        sidewaysInput = Input.GetAxis("Horizontal") * moveSpeed;

        transform.Translate(Vector3.forward * forwardInput * Time.deltaTime);
        transform.Translate(Vector3.right * sidewaysInput * Time.deltaTime);

        
    }
}
