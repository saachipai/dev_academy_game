using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    public float sensitiveity = 100;
    public Transform body;
    private float xRot= 0;
   
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,30, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            hit.collider.gameObject.transform.SetParent(this.gameObject.transform);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
        }
    }
}
