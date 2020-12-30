using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrickle : MonoBehaviour
{
    public GameObject player;
    private CharMove playerScript;
    public Camera playerCamera;
    public float startingDist= 10;
    public float movingRate = 1;
    public float killDist = 9;
    public Vector3 dirUnitVector;
    private Vector3[] dirs;
    // Start is called before the first frame update
    void Start()
    {
        dirs = new[] { transform.up, -transform.up, transform.right, -transform.right };
        playerScript = player.GetComponent<CharMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(player.transform);
        Vector3 monstLoc = getNearestSurface();
        float dist = Vector3.Magnitude(monstLoc - transform.GetChild(0).transform.position);
        Debug.Log(dist);
        transform.GetChild(0).transform.position = Vector3.Lerp(monstLoc, transform.GetChild(0).transform.position, dist * .05f * Time.deltaTime);
        //transform.GetChild(0).transform.position = monstLoc;

        Debug.Log(Mathf.Abs((monstLoc - transform.GetChild(0).transform.position).magnitude));
        //transform.LookAt(player.transform);
        if (playerScript.isSeen && (Mathf.Abs((player.transform.position - transform.position).magnitude) >= killDist))
        {

            
            //Debug.Log(dirUnitVector + "dirUnitVector:dist" + Mathf.Abs((player.transform.position - transform.position).magnitude));
            transform.Translate((transform.forward * movingRate * Time.deltaTime + transform.right * Mathf.Sin(Time.deltaTime* movingRate)) );
            //transform.Translate(0, 0, 1);
            //Debug.Log("spider Move");

        }
        else
        {
            transform.position = player.transform.position + new Vector3(2,0,2);//Vector3.Scale(dirUnitVector, dirUnitVector);
        }
        if (Mathf.Abs((player.transform.position - transform.position).magnitude) < killDist ){
            //Debug.Log("kill");
            //Debug.Log(Mathf.Abs((player.transform.position - transform.position).magnitude));
            transform.position = player.transform.position + (player.transform.forward * -startingDist)- dirUnitVector;
            //Destroy(this);

        }
    }
    void reset()
    {
        transform.position =  -playerCamera.transform.TransformDirection(Vector3.forward)* startingDist;
        //transform.position = -player.transform.forward * startingDist;
    }

    Vector3 getNearestSurface()
    {
        int layerMask = 1 << 9;
        Vector3 endPos = transform.position;
        float getDist = -1;
        for (int i = 0; i < 4; i++)
        {

            RaycastHit hit;
            Ray distWall = new Ray(transform.position, dirs[i]);
            if (Physics.Raycast(distWall, out hit, Mathf.Infinity, layerMask))
            {
                
                 if (getDist == -1)
                 {
                     getDist = hit.distance;
                     endPos = hit.point;
                 }
                 else if(getDist > hit.distance)
                 {
                     endPos = hit.point;
                 }
             }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.black);

            //endPos = hit.point;
        }
           
            Debug.Log(endPos);
        
        return endPos;
    }
}
