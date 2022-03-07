using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundedScript : MonoBehaviour
{
    private playerMovement _pm;

    void Start()
    {
        _pm = GetComponentInParent<playerMovement>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Groundtag" || other.tag == "SemisolidTag")
        {
            _pm.isGrounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Groundtag" || other.tag == "SemisolidTag")
        {
            _pm.isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Groundtag" || other.tag == "SemisolidTag")
        {
            _pm.isGrounded = false;
        }
    }
}
