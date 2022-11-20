using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    public GameObject[] redItems;
    public GameObject[] blueItems;
    private SpriteRenderer redSprite;
    private SpriteRenderer blueSprite;
    private Collider2D redCollider;
    private Collider2D blueCollider;
    private float redTrans = 1.0f;
    private float blueTrans = 0.4f;
    public bool isRedActive = true;
    public GameObject[] bgs;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        redItems = GameObject.FindGameObjectsWithTag("Red");
        blueItems = GameObject.FindGameObjectsWithTag("Blue");
        bgs = GameObject.FindGameObjectsWithTag("Background");

        UpdateColour();
            
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
                blueTrans = 0.4f;
            }
            else if (!isRedActive)
            {
                redTrans = .4f;
                blueTrans = 1f;
            }

            UpdateColour();
            
        }
    }

    void UpdateColour() 
    {
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
        
    }
}
