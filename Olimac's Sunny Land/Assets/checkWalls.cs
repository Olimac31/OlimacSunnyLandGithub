using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkWalls : MonoBehaviour
{
    public bool isColliding;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Groundtag" || other.tag == "SemisolidTag")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Groundtag" || other.tag == "SemisolidTag")
        {
            isColliding = false;
        }
    }
}
