using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.LogWarning("F's in the chat boys");
        }
    }
}