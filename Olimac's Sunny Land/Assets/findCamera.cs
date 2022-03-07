using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    void Start()
    {
        target = GameObject.Find("retroCamera").GetComponent<Camera>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.transform.position.y, transform.position.z);
    }
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, target.transform.position.y, transform.position.z);
    }
}
