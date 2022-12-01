using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Character;
    private BoxCollider2D boxCollider;
    public float Speed = 0.5f;
    public bool canControl;
    public float jumpForce = 0.5f;
    private bool inAir = false;
    private bool onGem = false;

    public float h;
    
    public string[] levels;
    public bool alive = true;
    public bool endOfLevel = false;
    public float deadTime;
    public float deadLength = 0.5f;

    public int deathCount = 0;

    private Vector3 respawnPoint;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;

        canControl = true;
        Character = GetComponent<Rigidbody2D>();
        Character.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider = GetComponent<BoxCollider2D>();

        //inital spawn position
        respawnPoint = Character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive) {
            canControl = true;
        }
        //left right movement
         if(canControl)
        {
            h = Input.GetAxisRaw("Horizontal") * Speed;

            Character.transform.position += new Vector3(h, 0, 0) * Time.deltaTime;

            if (Mathf.Abs(h) > 0 && !inAir){
                if (!onGem && !audioManager.IsPlaying("walkStone")){
                    audioManager.Play("walkStone");
                }
                else if (onGem && !audioManager.IsPlaying("walkGem")){
                    audioManager.Play("walkGem");
                }
            }
            else if (h == 0 || inAir) {
                audioManager.Stop("walkStone");
                audioManager.Stop("walkGem");
            }
        }

        // jumping
        if(Input.GetKeyDown(KeyCode.Space) && !inAir && canControl){
            //Debug.Log("trying to jump"); 
            
            if (onGem) {
                audioManager.Play("jumpGem");
            }
            else {
                audioManager.Play("jumpStone");
            }

            Character.velocity = new Vector2(0f, jumpForce);
            
        }

        if (!alive && Time.unscaledTime >= deadTime + deadLength){

            if(endOfLevel == true){ //on end of level
                endOfLevel = false;
                Time.timeScale = 1.0f;

                //change levels
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            else { //on death
                ResetScene();
            }
        }
    }

    private void FixedUpdate()
    {
        /*Debug.DrawRay(transform.position + Vector3.right * boxCollider.size.x/2.5f, Vector2.down * 2, Color.yellow);   //one ray on the right side of the collider
        Debug.DrawRay(transform.position + Vector3.left * boxCollider.size.x/2.5f, Vector2.down * 2, Color.yellow);    //other ray on left side*/
        LayerMask ground = LayerMask.GetMask("Default");
        LayerMask gem = LayerMask.GetMask("Gem");
        LayerMask movePlatform = LayerMask.GetMask("MovePlatform");

        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * boxCollider.size.x/2.5f, -Vector2.up, 0.1f, ground);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.left * boxCollider.size.x/2.5f, -Vector2.up, 0.1f, ground);

        RaycastHit2D hitGem = Physics2D.Raycast(transform.position + Vector3.right * boxCollider.size.x/2.5f, -Vector2.up, 0.1f, gem);
        RaycastHit2D hit2Gem = Physics2D.Raycast(transform.position + Vector3.left * boxCollider.size.x/2.5f, -Vector2.up, 0.1f, gem);

        RaycastHit2D hitMove = Physics2D.Raycast(transform.position + Vector3.right * boxCollider.size.x/2.5f, -Vector2.up, 0.1f, movePlatform);
        RaycastHit2D hit2Move = Physics2D.Raycast(transform.position + Vector3.left * boxCollider.size.x/2.5f, -Vector2.up, 0.1f, movePlatform);

        if (hit.collider != null || hit2.collider != null) {    //as long as one of the rays is touching the ground, then you can jump
            inAir = false;
            onGem = false;
        }
        else if (hitGem.collider != null || hit2Gem.collider != null || 
                 hitMove.collider != null || hit2Move.collider != null) { //if the rays are touching the gems/moving platforms
            inAir = false;
            onGem = true;
        }
         else { 
            inAir = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //death
        if (col.gameObject.tag == "deathPlane") {
            deathCount ++;
            audioManager.Play("death");
            Time.timeScale = 0.0f;
            deadTime = Time.unscaledTime;
            alive = false;
        }

        //end of level
        if (col.gameObject.tag == "Finish"){
            audioManager.Play("finish");
            endOfLevel = true;
            Animator princeAnimator = col.gameObject.GetComponent<Animator>();  //get animator of prince
            princeAnimator.SetBool("End", endOfLevel);  //set End to true to trigger transition to end sprite

            Time.timeScale = 0.0f;
            deadTime = Time.unscaledTime;
            canControl = false;
            alive = false;
        }

         if(col.gameObject.tag == "checkpoint"){
            if (col.GetComponent<SpriteRenderer>().color != new Color(0.9056604f, 0.9058824f, 0.4135279f, 1)) { //if checkpoint has not been reached yet
                audioManager.Play("checkpoint");
                respawnPoint = col.transform.position;
                col.GetComponent<SpriteRenderer>().color = new Color(0.9056604f, 0.9058824f, 0.4135279f, 1); //make checkpoint yellow
            }
         }
    }

    void ResetScene()
    {
        // Reset to respawn position
        transform.position = respawnPoint;
        audioManager.Play("respawn");

        // Help flag, indicates the normal play mode
        alive = true;


        // Unpause the game, FixedUpdate is called again every frame.
        Time.timeScale = 1.0f;
    }
}
