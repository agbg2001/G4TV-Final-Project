using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Character;
    public float Speed = 0.5f;
    public bool canControl;
    public float jumpForce = 0.5f;
    private bool inAir = false;

    public float h;
    
    public bool alive = true;
    public bool endOfLevel = false;
    public float deadTime;
    public float deadLength = 0.5f;

    private Vector3 respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        canControl = true;
        Character = GetComponent<Rigidbody2D>();
        Character.constraints = RigidbodyConstraints2D.FreezeRotation;

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
        }

        // jumping
        if(Input.GetKeyDown(KeyCode.Space) && !inAir && canControl){
            Debug.Log("trying to jump"); 
            
            Character.velocity = new Vector2(0f, jumpForce) * Speed;
            
        }

        if (!alive && Input.anyKey && Time.unscaledTime >= deadTime + deadLength){

            if(endOfLevel == true){ //on end of level
                //change levels (use an array?)
                if(SceneManager.GetActiveScene().name == "Level 1"){
                    SceneManager.LoadScene("SwapTest");
                }else SceneManager.LoadScene("Level 1");

                endOfLevel = false;
                Time.timeScale = 1.0f;
            }

            else { //on death
                ResetScene();
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.down * 2, Color.yellow);
        LayerMask ground = LayerMask.GetMask("Default");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, ground);

        if (hit.collider != null) {
            inAir = false;
            Debug.Log(hit.collider); 
        } else { 
            inAir = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //death
        if (col.gameObject.tag == "deathPlane") {
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
            respawnPoint = Character.transform.position;
         }
    }

    void ResetScene()
    {
        // Reset to respawn position
        transform.position = respawnPoint;

        // Help flag, indicates the normal play mode
        alive = true;

        // Reset the background
       
       

        // Unpause the game, FixedUpdate is called again every frame.
        Time.timeScale = 1.0f;
    }
}
