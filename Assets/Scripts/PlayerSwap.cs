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
    void Start()
    {
            redItems = GameObject.FindGameObjectsWithTag("Red");
            blueItems = GameObject.FindGameObjectsWithTag("Blue");
            redMoveItems = GameObject.FindGameObjectsWithTag("RedMove");
            blueMoveItems = GameObject.FindGameObjectsWithTag("BlueMove");

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
                blueTrans = .4f;
                redSpeed = 1f;
                blueSpeed = 0f;
            }
            else if (!isRedActive)
            {
                redTrans = .4f;
                blueTrans = 1f;
                redSpeed = 0f;
                blueSpeed = 1f;
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
            
            
            for (int i = 0; i < redMoveItems.Length; i++)
            {
                //redItems[i].SetActive(isRedActive);
                redSprite = redMoveItems[i].GetComponent<SpriteRenderer>();
                var redScriptSpeed = redMoveItems[i].GetComponent<MovingPlatform>();
                redScriptSpeed.speed = redSpeed;
                redSprite.color = new Color(1f, 1f, 1f, redTrans);

            }
            for (int i = 0; i < blueMoveItems.Length; i++)
            {
                //blueItems[i].SetActive(!isRedActive);
                blueSprite = blueMoveItems[i].GetComponent<SpriteRenderer>();
                var blueScriptSpeed = blueMoveItems[i].GetComponent<MovingPlatform>();
                blueScriptSpeed.speed = blueSpeed;
                blueSprite.color = new Color(1f, 1f, 1f, blueTrans);
            }

        }
    }
}
