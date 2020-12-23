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
        Debug.Log(yaw + "yaw:pitch" + pitch);
        xRot -= yaw;

        xRot = Mathf.Clamp(xRot, -90, 90);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        body.Rotate(pitch * Vector3.up);


    }
}
