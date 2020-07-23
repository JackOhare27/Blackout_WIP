using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //For eye animation
    public Image OpenEye;
    public Image ClosedEye;
    private bool isEyeOpen;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        isEyeOpen = FindObjectOfType<EnemyController>().isInFov;
        
        if(isEyeOpen == true)
        {
            OpenEye.enabled = true;
            ClosedEye.enabled = false;

        }else if (isEyeOpen == false)
        {
            OpenEye.enabled = false;
            ClosedEye.enabled = true;

        }
    }
}
