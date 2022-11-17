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
    void Start()
    {
        redItems = GameObject.FindGameObjectsWithTag("Red");
        blueItems = GameObject.FindGameObjectsWithTag("Blue");

        UpdateColour();
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
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
    }
}
