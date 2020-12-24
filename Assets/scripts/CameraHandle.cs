using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    public float sensitiveity = 100;
    public Transform body;
    private float xRot = 0;
    //private bool isHold;
    public GameObject item = null;
    private Camera cam;

    public Shader glow;
    public Shader normalShader;
    public GameObject tempItem;
    private float reload=0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
 
    }

    // Update is called once per frame
    void Update()
    {
        float yaw = Input.GetAxis("Mouse Y") * sensitiveity * Time.deltaTime;
        float pitch = Input.GetAxis("Mouse X") * sensitiveity * Time.deltaTime;
        xRot -= yaw;

        xRot = Mathf.Clamp(xRot, -90, 90);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        body.Rotate(pitch * Vector3.up);
        int layerMask = 1 << 8;

        RaycastHit hit;
        if (item == null)
        {

            
            Vector3 CameraCenter = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane));
            if (Physics.Raycast(CameraCenter, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                Debug.DrawRay(CameraCenter, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                tempItem = hit.collider.gameObject;
                normalShader = tempItem.GetComponent<Renderer>().material.shader;
                tempItem.GetComponent<Renderer>().material.shader = glow;
                
                if (Input.GetButton("Fire1")) { 
                    reload = Time.time + 1;
                    //with(item)
                    //{
                        item = tempItem;
                        item.transform.SetParent(this.gameObject.transform);
                        //item.GetComponent<Rigidbody>().isKinematic = false;
                        item.GetComponent<Rigidbody>().useGravity = false;
                        item.GetComponent<Collider>().enabled = false;
                    //item.GetComponent<Renderer>().material.shader = glow;
                    //}


                }

            }
            else
            {
                Debug.DrawRay(CameraCenter, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);
               
                if (tempItem != null)
                {
                    Debug.Log("switch shades back");
                    tempItem.GetComponent<Renderer>().material.shader = normalShader;
                    tempItem = null;
                }
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                //using (item)
                //{
                item.GetComponent<Collider>().enabled = true;
                //item.GetComponent<Rigidbody>().isKinematic = true;
                item.GetComponent<Rigidbody>().useGravity = true;
                //item.GetComponent<Renderer>().material.shader = normalShader;
                item.transform.parent = null;
               
                //}

                item = null;
            }
        }
    }
}


