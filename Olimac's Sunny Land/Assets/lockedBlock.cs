using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedBlock : MonoBehaviour
{
    public Collider2D myCollider;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.globalLockedBlocks)
        {
            myCollider.enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            myCollider.enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
