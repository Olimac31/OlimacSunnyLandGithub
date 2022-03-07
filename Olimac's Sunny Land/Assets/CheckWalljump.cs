using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalljump : MonoBehaviour
{
    public bool isColliding;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Groundtag")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Groundtag")
        {
            isColliding = false;
        }
    }
}
