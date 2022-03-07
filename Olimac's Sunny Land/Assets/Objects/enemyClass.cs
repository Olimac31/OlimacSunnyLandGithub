using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyClass : MonoBehaviour
{
    public int enemyHealth;
    public bool canStomp;
    public GameObject colLeft, colRight, hitbox, groundCheck; //Left and Right Colliders for walls
    
    public Rigidbody2D rbody;
    Collider2D myCollider;
    SpriteRenderer myRenderer;

    playerMovement thePlayer;

    public float speedX, speedY;
    public float myDamage = 1f;

    public bool isDead = false;

    public RuntimeAnimatorController deathAnimation;
    public AudioClip hitSound;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();

        thePlayer = GameObject.Find("objPlayer").GetComponent<playerMovement>(); //Player's main variables
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Alive state-------------------------------------------------------------------------
        if(!isDead)
        {
            rbody.velocity = new Vector2(speedX, rbody.velocity.y);

            //Die if stomped (Default way of killing an enemy)
            if(canStomp && hitbox.GetComponent<enemyHitbox>().isColliding && thePlayer.speedY < -0.5f && thePlayer.transform.position.y > transform.position.y - 0.1f)
            {
                //Extra jump boost
                if(thePlayer.keyJump)
                {
                    thePlayer.playerJump(thePlayer.jumpSpeed, hitSound);
                }
                else
                {
                    thePlayer.playerJump(thePlayer.jumpSpeed * 0.5f, hitSound);
                }

                enemyDie();
            }
            //Hurt the player
            else if(hitbox.GetComponent<enemyHitbox>().isColliding && thePlayer.myHealth.canHurt)
            {
                thePlayer.playerJump(thePlayer.jumpSpeed/2, null);
                thePlayer.speedX = Mathf.Sign(thePlayer.speedX) * -15f;

                thePlayer.myHealth.hurtPlayer(myDamage);
            }
        }
        //Dead State------------------------------------------------
        else
        {
            rbody.velocity = new Vector2(0, 0);
        }
    }

    public void setSpriteFlip(bool value) //Flip the sprite
    {
        myRenderer.flipX = value;
    }

    public void setAnimationSpeed(float value) //Set an animation speed
    {
        GetComponent<Animator>().speed = value;
    }

    public void enemyDie() //Die
    {
        GetComponent<Animator>().runtimeAnimatorController = deathAnimation;
        speedX = 0;
        speedY = 0;
        rbody.velocity = new Vector2(0, 0);
        rbody.gravityScale = 0;
        
        isDead = true;
        Destroy(gameObject, 0.35f);
    }

    public void setAnimationBool(string name, bool value)
    {
        GetComponent<Animator>().SetBool(name, value);
    }
}
