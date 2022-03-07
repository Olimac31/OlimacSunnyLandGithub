using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target;
    public Vector2 offset = new Vector2(0.25f, -0.25f);

    public float camLimitUp, camLimitDown, camLimitLeft, camLimitRight;

    bool cameraWaiting = true;
    int cameraWaitingTime = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Fix odd bug where objects load weirdly for one frame
        if(cameraWaiting)
        {
            turnOnCamera();
        }

        //HORIZONTAL LIMIT
        if(target.transform.position.x <= camLimitRight && target.transform.position.x >= camLimitLeft)
        {
            transform.position = new Vector3(target.transform.position.x + offset.x, transform.position.y, transform.position.z);
        }
        else
        {
            if(target.transform.position.x > camLimitRight) transform.position = new Vector3(camLimitRight, transform.position.y, transform.position.z);
            if(target.transform.position.x < camLimitLeft) transform.position = new Vector3(camLimitLeft, transform.position.y, transform.position.z);
        }

        //VERTICAL LIMIT
        if(target.transform.position.y <= camLimitUp && target.transform.position.y >= camLimitDown)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y + offset.y, transform.position.z);
        }
        else
        {
            if(target.transform.position.y > camLimitUp) transform.position = new Vector3(transform.position.x, camLimitUp, transform.position.z);
            if(target.transform.position.y < camLimitDown) transform.position = new Vector3(transform.position.x, camLimitDown, transform.position.z);
        }
        //transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);

        //FIX SCREEN JITTERING
        //transform.position = PixelPerfectClamp(target.transform.position, GameManager.instance.GlobalPixelsPerUnit);
    }

    void turnOnCamera()
    {
        //Camera starts disabled to allow for objects to load first
        if(cameraWaitingTime > 0)
        {
            cameraWaitingTime--;
        }
        else
        {
            GetComponent<Camera>().enabled = true;
            cameraWaiting = false;
        }
    }

    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit), Mathf.CeilToInt(locationVector.y * pixelsPerUnit), Mathf.CeilToInt(transform.position.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }
}
