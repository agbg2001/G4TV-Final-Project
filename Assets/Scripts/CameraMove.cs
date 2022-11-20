using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private PlayerMovement player;
    public float maxRight;
    public float maxLeft;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        // start moving camera before first frame or it looks weird
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(player.transform.position.x, maxLeft, maxRight);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the Camera when the player moves
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(player.transform.position.x, maxLeft, maxRight);
        transform.position = pos;
    }
}
