using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyItem : MonoBehaviour
{
    bool taken = false;
    public AudioClip keySFX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !taken)
        {
            SoundManager.instance.PlaySound(keySFX);
            GameManager.instance.globalLockedBlocks = false;

            taken = true;
            Destroy(gameObject);
        }
    }
}
