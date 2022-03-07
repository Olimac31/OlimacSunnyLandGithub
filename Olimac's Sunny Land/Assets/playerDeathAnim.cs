using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeathAnim : MonoBehaviour
{
    Animator myAnims;
    public AudioClip deathSound;

    public float waitingTime = 10f;
    public int phase = 0;


    // Start is called before the first frame update
    void Start()
    {
        myAnims = GetComponent<Animator>();
        myAnims.SetBool("exploded", false);
    }

    // Update is called once per frame
    void Update()
    {
        myAnims.speed = 0.2f;
        if(waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
        }
        
        if(!myAnims.GetBool("exploded") && waitingTime <= 0 && phase == 0)
        {
            phase++;
            waitingTime = 3f;
            myAnims.SetBool("exploded", true);
            SoundManager.instance.PlaySound(deathSound);
        }
    }
}
