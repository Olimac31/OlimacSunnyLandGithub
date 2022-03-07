using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeInvisibleSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
