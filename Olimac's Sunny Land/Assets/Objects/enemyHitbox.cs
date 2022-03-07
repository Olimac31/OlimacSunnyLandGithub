using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitbox : MonoBehaviour
{
    public bool isColliding;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isColliding = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isColliding = false;
        }
    }
}
