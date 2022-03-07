using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyOpossum : MonoBehaviour
{
    enemyClass enemy;

    public float walkSpeed = 4f;
    int side = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<enemyClass>();
    }

    // Update is called once per frame
    void Update()
    {
        opossumMovement();
        opossumAnimation();
    }

    void opossumMovement()
    {
        bool isColLeft = enemy.colLeft.GetComponent<checkWalls>().isColliding;
        bool isColRight = enemy.colRight.GetComponent<checkWalls>().isColliding;

        if(isColLeft)
        {
            side = 1;
        }
        if(isColRight)
        {
            side = -1;
        }

        enemy.speedX = walkSpeed * side;
    }

    void opossumAnimation()
    {
        enemy.setAnimationSpeed(0.3f);
        if(side == 1)
        {
            enemy.setSpriteFlip(true);
        }
        else
        {
            enemy.setSpriteFlip(false);
        }
    }
}
