using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyEagle : MonoBehaviour
{

    enemyClass enemy;
    public float speed;
    public Transform[] moveSpots;
    int spotIndex = 0;

    public bool inReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<enemyClass>();
    }

    // Update is called once per frame
    void Update()
    {
        eagleMovement();
        eagleAnimation();
    }

    private void eagleMovement()
    {
        if(!enemy.isDead)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[spotIndex].position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, moveSpots[spotIndex].position) < 0.2f)
            {
                if(spotIndex < moveSpots.Length - 1)
                {
                    spotIndex++;
                }
                else if(spotIndex >= moveSpots.Length - 1)
                {
                    spotIndex = 0;
                    if(inReverse)
                    {
                        Array.Reverse(moveSpots);
                    }
                }
            }
        }
    }

    private void eagleAnimation()
    {
        enemy.setAnimationSpeed(0.2f);

        if(transform.position.x < moveSpots[spotIndex].position.x)
        {
            enemy.setSpriteFlip(true);
        }
        else
        {
            enemy.setSpriteFlip(false);
        }
    }
}
