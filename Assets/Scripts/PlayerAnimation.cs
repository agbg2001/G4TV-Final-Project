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
        //purple if end of level
        if (playerMovement.endOfLevel) {
            spriteRenderer.color = new Color(0.6862745f, 0.4627451f, 0.8313726f, 1); 
        }
        else if (playerSwap.isRedActive) {
            spriteRenderer.color = new Color(0.9098039f, 0.1019608f, 0.3058824f, 1);   //turns sprite red
        }
        else {
            spriteRenderer.color = new Color(0.1058824f, 0.5764706f, 0.9137255f, 1);   //turns sprite blue
        }


        //Flip sprite when moving left
        if (playerMovement.h < 0) {
            transform.localScale = new Vector3(-.75f, .75f, .75f);
        }
        else if (playerMovement.h > 0) {
            transform.localScale = new Vector3(.75f, .75f, .75f);
        }

        animator.SetFloat("PlayerSpeed", Mathf.Abs(playerMovement.h));   //set Speed = absolute val of horizontal movement speed
        animator.SetFloat("Vertical", playerMovement.Character.velocity.y); //get vertical speed of player
    }
}
