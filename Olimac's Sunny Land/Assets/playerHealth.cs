using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public string sceneToLoad;
    public float safeFramesMAX = 2;
    float safeFrames = 0;

    public bool canHurt = true;

    public AudioClip hurtSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(safeFrames > 0)
        {
            safeFrames -= Time.deltaTime;
            canHurt = false;
        }
        else
        {
            canHurt = true;
        }

        HurtEffect();
    }

    public void hurtPlayer(float damage)
    {
        //Apply damage to the Game Manager
        GameManager.instance.playerHP -= damage;
        safeFrames = safeFramesMAX;
        SoundManager.instance.PlaySound(hurtSound);
        checkDead();

        //Give the player invincibility frames
        canHurt = false;

        //Reflect changes on the GUI
        GameManager.instance.updateCustomGUI();
    }

    public void checkDead()
    {
        if(GameManager.instance.playerHP <= 0)
        {
            GetComponent<playerMovement>().stopMovement();
            GameManager.instance.playerDeath();
        }
    }

    void HurtEffect()
    {
        if(safeFrames > 1.8f)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.r = 255;
            tmp.g = 0;
            tmp.b = 0;
            tmp.a = 1f;
            GetComponent<SpriteRenderer>().color = tmp;
        }
        else if (safeFrames > 0.1f)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.r = 255;
            tmp.g = 255;
            tmp.b = 255;
            tmp.a = 0.5f;
            GetComponent<SpriteRenderer>().color = tmp;
        }
        else
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.r = 255;
            tmp.g = 255;
            tmp.b = 255;
            tmp.a = 1f;
            GetComponent<SpriteRenderer>().color = tmp;
        }
    }
}
