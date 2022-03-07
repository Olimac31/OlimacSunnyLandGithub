using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGem : MonoBehaviour
{
    bool taken = false;
    public AudioClip gemSFX;

    void Start()
    {
        GetComponent<Animator>().speed = 0.2f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !taken)
        {
            GetComponent<Animator>().SetBool("taken", true);
            SoundManager.instance.PlaySound(gemSFX);
            GameManager.instance.globalDiamonds++;

            taken = true;
            Destroy(gameObject, 0.35f);

            GameManager.instance.updateCustomGUI();
        }
    }
}
