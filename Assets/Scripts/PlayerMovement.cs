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

    private Vector3 respawnPoint;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        canControl = true;
        Character = GetComponent<Rigidbody2D>();
        Character.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider = GetComponent<BoxCollider2D>();

        //inital spawn position
        respawnPoint = Character.transform.position;
        levels = new string [] {"Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9", "Level 10"};
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

            Character.velocity = new Vector2(0f, jumpForce) * Speed;
            
        }

        if (!alive && Time.unscaledTime >= deadTime + deadLength){

            if(endOfLevel == true){ //on end of level

                endOfLevel = false;
                Time.timeScale = 1.0f;

                //change levels (use an array?)
                for (int i = 0; i < levels.Length; i ++){
                    Debug.Log(i);
                    if(SceneManager.GetActiveScene().name == levels[i]){

                        Debug.Log("level checked"); 
                        
                        if (i + 1 == levels.Length){ //currently, if you beat all the levels, it resets to level 1
                            SceneManager.LoadScene(levels[0]);
                        }
                        else { 
                            SceneManager.LoadScene(levels[i + 1]); 
                        }
                        
                    }
                }
            }

            else { //on death
                ResetScene();
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position + Vector3.right * boxCollider.size.x/2, Vector2.down * 2, Color.yellow);   //one ray on the right side of the collider
        Debug.DrawRay(transform.position + Vector3.left * boxCollider.size.x/2, Vector2.down * 2, Color.yellow);    //other ray on left side
        LayerMask ground = LayerMask.GetMask("Default");
        LayerMask gem = LayerMask.GetMask("Gem");

        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.right * boxCollider.size.x/2, -Vector2.up, 0.1f, ground);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.left * boxCollider.size.x/2, -Vector2.up, 0.1f, ground);

        RaycastHit2D hitGem = Physics2D.Raycast(transform.position + Vector3.right * boxCollider.size.x/2, -Vector2.up, 0.1f, gem);
        RaycastHit2D hit2Gem = Physics2D.Raycast(transform.position + Vector3.left * boxCollider.size.x/2, -Vector2.up, 0.1f, gem);

        if (hit.collider != null || hit2.collider != null) {    //as long as one of the rays is touching the ground, then you can jump
            inAir = false;
            onGem = false;
        }
        else if (hitGem.collider != null || hit2Gem.collider != null) { //if the rays are touching the gems
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
            audioManager.Play("death");
            Time.timeScale = 0.0f;
            deadTime = Time.unscaledTime;
            alive = false;
        }

        //end of level
        if (col.gameObject.tag == "Finish"){
            Time.timeScale = 0.0f;
            deadTime = Time.unscaledTime;

            endOfLevel = true;
            canControl = false;
            alive = false;

            Debug.Log("this is the end"); 
        }

         if(col.gameObject.tag == "checkpoint"){
            respawnPoint = col.transform.position;
            col.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1); //make checkpoint yellow
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
