using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 mov;
    public float movX;
    public bool keyJump, keyJumpDown, keyJumpUp;
    
    Rigidbody2D rbody;
    Collider2D myCollider;
    playerAudio myAudio;
    public playerHealth myHealth;
    
    public bool reading = false;
    
    public bool isGrounded = false;
    public GameObject colLeft;
    public GameObject colRight;
    float wallJumpLock = 0;
    float wallJumpLockMAX = 10f;
    int wallJumpSide = 0;

    public float jumpSpeed = 18f;
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float maxVerticalSpeed = -15f;
    
    public float speedX = 0;
    public float speedY = 0;
    [SerializeField] private float movingSpeed = 0.25f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAudio = GetComponent<playerAudio>();
        myHealth = GetComponent<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detectar inputs horizontales
        if(!reading)
        {
            //Continuous inputs
            movX = Input.GetAxisRaw("Horizontal");
            keyJump = Input.GetButton("Jump");

            //Instantaneous inputs
            if(Input.GetButtonDown("Jump"))
            {
                keyJumpDown = true;
            }

            if(Input.GetButtonUp("Jump"))
            {
                keyJumpUp = true;
            }
        }
    }
    void FixedUpdate()
    {
        if(!reading)
        {
            //JUMPING
            if(keyJump && isGrounded && rbody.velocity.y < 1)
            {
                playerJump(jumpSpeed);
            }

            //JUMP CANCEL
            if(keyJumpUp && !isGrounded && rbody.velocity.y > 1)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * 0.5f);
            }

            //WALLJUMP
            if(!isGrounded && keyJumpDown)
            {
                //wallJumpSide is used to avoid letting the player climb on a single wall
                if(colLeft.GetComponent<CheckWalljump>().isColliding)
                {
                    speedX = maxSpeed * 1.15f;
                    playerJump(jumpSpeed * 0.7f, myAudio.kickSound);
                    GetComponent<SpriteRenderer>().flipX = false;
                    wallJumpLock = wallJumpLockMAX;
                    wallJumpSide = 1;
                }
                else if(colRight.GetComponent<CheckWalljump>().isColliding)
                {
                    speedX = -maxSpeed * 1.15f;
                    playerJump(jumpSpeed * 0.7f, myAudio.kickSound);
                    GetComponent<SpriteRenderer>().flipX = true;
                    wallJumpLock = wallJumpLockMAX;
                    wallJumpSide = -1;
                }
            }
            if(!isGrounded && wallJumpLock > 0)
            {
                wallJumpLock -= 1f;
                speedX = maxSpeed * wallJumpSide;
            }

            //INERTIA CORRECTION
            if(colLeft.GetComponent<CheckWalljump>().isColliding && movX == 1 && speedX < -0.1f)
            {
                speedX = 0.1f;
            }
            if(colLeft.GetComponent<CheckWalljump>().isColliding && movX == -1 && speedX < -0.1f)
            {
                speedX = -0.1f;
            }

            if(colRight.GetComponent<CheckWalljump>().isColliding && movX == -1 && speedX > 0.1f)
            {
                speedX = -0.1f;
            }
            if(colRight.GetComponent<CheckWalljump>().isColliding && movX == 1 && speedX > 0.1f)
            {
                speedX = 0.1f;
            }


            //ACCELERATION AND FRICTION
            if(movX != 0)
            {
                if(movX == 1 && speedX < maxSpeed)
                {
                    speedX += movingSpeed;
                }
                if(movX == -1 && speedX > -maxSpeed)
                {
                    speedX -= movingSpeed;
                }
            }
            else if (isGrounded)
            {
                if(speedX > 0) speedX -= movingSpeed;
                if(speedX < 0) speedX += movingSpeed;
            }
            //DEADZONE
            if(speedX < movingSpeed && speedX > -movingSpeed)
            {
                speedX = 0;
            }

            //SPEED LIMIT
            if(rbody.velocity.y < maxVerticalSpeed)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, maxVerticalSpeed);
            }

            //LAND ON THE FLOOR
            if(isGrounded) //Reset all variables related to player gimmicks
            {
                wallJumpSide = 0;
                wallJumpLock = 0;
            }
            
            speedY = rbody.velocity.y;
            //APPLYING VELOCITY
            rbody.velocity = new Vector2(speedX, rbody.velocity.y);
        }
        //Set back instantaneous inputs to false at the end of the FixedUpdate
        if(keyJumpDown) keyJumpDown = false;
        if(keyJumpUp) keyJumpUp = false;
    }

    public void playerJump(float Yspeed)
    {
        rbody.velocity = new Vector2(rbody.velocity.x, Yspeed);
        SoundManager.instance.PlaySound(myAudio.jumpSound);
    }
    public void playerJump(float Yspeed, AudioClip altSound)
    {
        rbody.velocity = new Vector2(rbody.velocity.x, Yspeed);
        SoundManager.instance.PlaySound(altSound);
    }

    public void stopMovement()
    {
        reading = true;
        rbody.velocity = new Vector2(0, rbody.velocity.y);
        movX = 0;
    }

    public void placeholderDie()
    {
        rbody.velocity = new Vector2(-rbody.velocity.x, jumpSpeed * 0.5f);
    }
}
