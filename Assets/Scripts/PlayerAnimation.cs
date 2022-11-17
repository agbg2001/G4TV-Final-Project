using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovement;  //movement script
    PlayerSwap playerSwap; //swap script
    SpriteRenderer spriteRenderer;
    public Animator animator;   //reference to animator component
    public GameObject character;    //reference to player object

    void Awake()
    {
        playerMovement = character.GetComponent<PlayerMovement>();  //Access the movement script
        playerSwap = character.GetComponent<PlayerSwap>();  //access swap script
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //change sprite colour based on active colour
        if (playerSwap.isRedActive) {
            spriteRenderer.color = new Color(1, 0, 0, 1);   //turns sprite red
        }
        else {
            spriteRenderer.color = new Color(0, 0, 1, 1);   //turns sprite blue
        }


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
