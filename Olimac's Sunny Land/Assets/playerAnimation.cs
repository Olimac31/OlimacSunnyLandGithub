using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    playerMovement myPlayer;
    Animator myAnimator;

    SpriteRenderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<playerMovement>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.speed = 0.2f;

        //FLIPPING ANIMATION
        if(myPlayer.movX > 0) myRenderer.flipX = false;
        if(myPlayer.movX < 0) myRenderer.flipX = true;

        //MOVING OR STANDING STILL
        if(myPlayer.movX > 0.1f || myPlayer.movX < -0.1f)
        {
            myAnimator.SetBool("standing", false);
        }
        else
        {
            myAnimator.SetBool("standing", true);
        }

        //JUMPING
        if(!myPlayer.isGrounded)
        {
            myAnimator.SetBool("isOnAir", true);

            //FALLING ANIMATION
            if(myPlayer.speedY < -0.1f)
            {
                myAnimator.SetBool("isFalling", true);
            }
            else
            {
                myAnimator.SetBool("isFalling", false);
            }
        }
        else
        {
            myAnimator.SetBool("isOnAir", false);
        }
    }
}
