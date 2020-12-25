using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event01 : MonoBehaviour
{
    public triggerSelect trigger;
    //public Material mat;
    //public Material mat2;
    //private Renderer rend;
    public float exampleVal=1;
    // Start is called before the first frame update
    void Start()
    {
        //rend = 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (trigger.isTriggered == true)
        {
            transform.position = new Vector3(0, Mathf.Sin(Time.time)* exampleVal, 0)  + transform.position;
        }
        else
        {
            transform.position =new Vector3(0, 0, 0)+ transform.position;
        }
    }
    
}
