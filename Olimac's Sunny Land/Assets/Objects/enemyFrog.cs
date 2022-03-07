using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFrog : MonoBehaviour
{
    enemyClass enemy;

    public float jumpSpeed;
    public float walkSpeed;
    int side = 1;

    float waitingTime;
    bool avoidedFalling = false;
    public float fallDistance;
    public bool canAvoidPits;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<enemyClass>();
        setWaitingTime();
    }

    // Update is called once per frame
    void Update()
    {
        frogMovement();
        frogAnimation();
    }

    private void setWaitingTime()
    {
        float randomTime = Random.Range(0.5f, 2.5f);
        waitingTime = randomTime;
    }

    private void frogMovement()
    {
        //Booleans
        bool isColLeft = enemy.colLeft.GetComponent<checkWalls>().isColliding;
        bool isColRight = enemy.colRight.GetComponent<checkWalls>().isColliding;
        bool isGrounded = enemy.groundCheck.GetComponent<checkWalls>().isColliding;
        if(!enemy.isDead)
        {
            //Waiting to jump
            if(waitingTime > 0 && isGrounded)
            {
                waitingTime -= Time.deltaTime;
                enemy.speedX = 0;
            }
            if(!isGrounded && !avoidedFalling)
            {
                enemy.speedX = walkSpeed * side;
            }

            //Jump
            if(waitingTime <= 0 && isGrounded)
            {
                enemy.speedX = walkSpeed * side;
                enemy.rbody.velocity = new Vector2(enemy.speedX, jumpSpeed);

                setWaitingTime();
            }

            //Changing sides
            //LEFT
            if(isColLeft && !isGrounded && side == -1)
            {
                enemy.speedX = -enemy.speedX;
                side = 1;
            }
            //RIGHT
            if(isColRight && !isGrounded && side == 1)
            {
                enemy.speedX = -enemy.speedX;
                side = -1;
            }

            //Avoid pits (Beta)
            if(canAvoidPits)
            {
                RaycastHit2D rayGround = Physics2D.Raycast(new Vector2(transform.position.x + 0.1f * side, transform.position.y), Vector2.down, fallDistance, enemy.groundLayer);
                Debug.DrawRay(new Vector2(transform.position.x + 0.1f * side, transform.position.y), new Vector2(0, -fallDistance), Color.red, fallDistance);

                if(!rayGround && !isGrounded && !avoidedFalling)
                {
                    avoidedFalling = true;
                }
                if(avoidedFalling && !isGrounded)
                {
                    enemy.speedX = walkSpeed * -side * 0.5f;
                }

                if(avoidedFalling && isGrounded)
                {
                    side = -side;
                    avoidedFalling = false;
                }
            }
        }
    }
    private void frogAnimation()
    {
        bool isGrounded = enemy.groundCheck.GetComponent<checkWalls>().isColliding;
        enemy.setAnimationSpeed(0.2f);

        if(side == 1)
        {
            enemy.setSpriteFlip(true);
        }
        else
        {
            enemy.setSpriteFlip(false);
        }

        enemy.setAnimationBool("isGrounded", isGrounded);

        if(enemy.rbody.velocity.y > -1)
        {
            enemy.setAnimationBool("isFalling", false);
        }
        else
        {
            enemy.setAnimationBool("isFalling", true);
        }
    }
}
