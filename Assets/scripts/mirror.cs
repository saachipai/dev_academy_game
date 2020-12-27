using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirror : MonoBehaviour
{
    public Camera cam;
    public Transform mirror2;
    public GameObject target;
    public bool onScreen = false;
    private bool oldOnScreen = false;
    private GameObject doppleganger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        Vector3 screenPoint = cam.WorldToViewportPoint(target.transform.position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {

            Vector3 distFromCamera = transform.position - target.transform.position;
            //Debug.Log(distFromCamera + "<-cam dist" + transform.position +"<-camPos,traget->"+ target.transform.position);
            if (onScreen != oldOnScreen)
            {
                doppleganger = Instantiate(target, mirror2.localPosition + distFromCamera, transform.rotation);
                oldOnScreen = onScreen;
            }
            //Debug.Log(mirror2.rotation.eulerAngles);
            Quaternion yzSwitch = Quaternion.Euler(0, mirror2.rotation.eulerAngles.y, 0);
            Debug.Log(yzSwitch.eulerAngles);
            distFromCamera.z = -distFromCamera.z;
            doppleganger.transform.position = Vector3.Scale(mirror2.transform.position + transformRotation(yzSwitch, distFromCamera), new Vector3(1, 1, 1));// + new Vector3(1, target.transform.position.y, 1);
            //distFromCamera.x = -distFromCamera.x;
            //doppleganger.transform.position = mirror2.transform.localPosition + (distFromCamera);// + mirror2.transform.TransformDirection(Vector3.up) ;
        }
        else
        {
            oldOnScreen = onScreen;
            Destroy(doppleganger);
        }



    }
    /*
        void OnTriggerEnter(Collider col)
        {
            Debug.Log("enter");
                if (col.gameObject == target)
                {
                    col.gameObject.transform.position = mirror2.transform.position;
                }
        }
        */
    Vector3 transformRotation(Quaternion rotation, Vector3 coordinates)
    {
        Matrix4x4 m = Matrix4x4.Rotate(rotation);
        return m.MultiplyPoint3x4(coordinates);
    }

}

