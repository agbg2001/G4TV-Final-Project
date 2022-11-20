using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    public GameObject[] redItems;
    public GameObject[] blueItems;
    public GameObject[] redMoveItems;
    public GameObject[] blueMoveItems;
    private SpriteRenderer redSprite;
    private SpriteRenderer blueSprite;
    private Collider2D redCollider;
    private Collider2D blueCollider;
    private float redTrans;
    private float blueTrans;
    private float redSpeed;
    private float blueSpeed;
    public bool isRedActive = true;
    public GameObject[] bgs;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;

        redItems = GameObject.FindGameObjectsWithTag("Red");
        blueItems = GameObject.FindGameObjectsWithTag("Blue");
        redMoveItems = GameObject.FindGameObjectsWithTag("RedMove");
        blueMoveItems = GameObject.FindGameObjectsWithTag("BlueMove");
        bgs = GameObject.FindGameObjectsWithTag("Background");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            audioManager.Play("swap");

            isRedActive = !isRedActive;
            if (isRedActive)
            {
                redTrans = 1f;
                blueTrans = .4f;
                redSpeed = 10f;
                blueSpeed = 0f;
            }
            else if (!isRedActive)
            {
                redTrans = .4f;
                blueTrans = 1f;
                redSpeed = 0f;
                blueSpeed = 10f;
            }
            for (int i = 0; i < redItems.Length; i++)
            {
                //redItems[i].SetActive(isRedActive);
                redSprite = redItems[i].GetComponent<SpriteRenderer>();
                redItems[i].GetComponent<Collider2D>().enabled = isRedActive;
                redSprite.color = new Color(1f, 1f, 1f, redTrans);


            }
            for (int i = 0; i < blueItems.Length; i++)
            {
                //blueItems[i].SetActive(!isRedActive);
                blueSprite = blueItems[i].GetComponent<SpriteRenderer>();
                blueItems[i].GetComponent<Collider2D>().enabled = !isRedActive;
                blueSprite.color = new Color(1f, 1f, 1f, blueTrans);
            }

            for (int i = 0; i < bgs.Length; i++){
                if(isRedActive){
                    //change colour of bg to red
                        bgs[i].GetComponent<SpriteRenderer>().color = new Color(0.6981132f, 0.4504806f, 0.4504806f, 1);
                }
                else {
                    //change colour of bg to blue
                        bgs[i].GetComponent<SpriteRenderer>().color = new Color(0.4509804f, 0.5764555f, 0.6980392f, 1);
                }
            }
            
            
            for (int i = 0; i < redMoveItems.Length; i++)
            {
                //redItems[i].SetActive(isRedActive);
                redSprite = redMoveItems[i].GetComponent<SpriteRenderer>();
                var redScriptSpeed = redMoveItems[i].GetComponent<MovingPlatform>();
                //redScriptSpeed.speed = redSpeed;
                redSprite.color = new Color(1f, 1f, 1f, redTrans);
                if (isRedActive)
                {
                    redScriptSpeed.isActive = true;
                }
                else if (!isRedActive)
                {
                    redScriptSpeed.StopAllCoroutines();
                    redScriptSpeed.isActive = false;
                }

            }
            for (int i = 0; i < blueMoveItems.Length; i++)
            {
                //blueItems[i].SetActive(!isRedActive);
                blueSprite = blueMoveItems[i].GetComponent<SpriteRenderer>();
                var blueScriptSpeed = blueMoveItems[i].GetComponent<MovingPlatform>();
                //blueScriptSpeed.speed = blueSpeed;
                blueSprite.color = new Color(1f, 1f, 1f, blueTrans);
                if(isRedActive)
                {
                    blueScriptSpeed.isActive = false;
                    blueScriptSpeed.StopAllCoroutines();
                }
                else if(!isRedActive)
                {

                    blueScriptSpeed.isActive = true;
                }
            }

        }

        for (int i = 0; i < bgs.Length; i++){
            if(isRedActive){
                //change colour of bg to red
                    bgs[i].GetComponent<SpriteRenderer>().color = new Color(0.6981132f, 0.4504806f, 0.4504806f, 1);
            }
            else {
                //change colour of bg to blue
                    bgs[i].GetComponent<SpriteRenderer>().color = new Color(0.4509804f, 0.5764555f, 0.6980392f, 1);
            }
        }
        
    }
}
