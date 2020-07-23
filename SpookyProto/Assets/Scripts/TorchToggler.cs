using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchToggler : MonoBehaviour
{
    public Material[] TorchPowered;
    public bool isOn = false;
    public Light torchLight;
    public Collider torchStunDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If we press down, the light turns on; when we let go, it turns back off.
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<MeshRenderer>().material = TorchPowered[1];
            torchLight.intensity = 1;
            //Enables ability to stun enemy
            torchStunDistance.enabled = true;

        }else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Changes back to dark mesh
            GetComponent<MeshRenderer>().material = TorchPowered[0];
            //Turns off light
            torchLight.intensity = 0;
            torchStunDistance.enabled = false;
            
        }
    }
}
