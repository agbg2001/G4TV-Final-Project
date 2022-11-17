using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;
    public Animator animator;   //reference to animator component
    public GameObject character;    //reference to player object

    void Awake()
    {
        playerMovement = character.GetComponent<PlayerMovement>();  //Access the movement script
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Flip sprite when moving left
        if (playerMovement.h < 0) {
            spriteRenderer.flipX = true;
        }
        else if (playerMovement.h > 0) {
            spriteRenderer.flipX = false;
        }


        animator.SetFloat("PlayerSpeed", Mathf.Abs(playerMovement.h));   //set Speed = absolute val of horizontal movement speed
        animator.SetFloat("Vertical", playerMovement.Character.velocity.y); //get vertical speed of player
    }
}
