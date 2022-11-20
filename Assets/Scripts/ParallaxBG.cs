using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{

    float length;
    float start;

    public GameObject cam;
    public float parallax;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x; //starting position
        length = GetComponent<SpriteRenderer>().bounds.size.x;  //get size of the img

        float camDist = (cam.transform.position.x * (1-parallax));  //distance relative to the cam
        float distance = (cam.transform.position.x * parallax); //distance the layer moves relative to the world
        transform.position = new Vector3(start + distance, transform.position.y, transform.position.z); //move the bg along the x axis
    }

    // Update is called once per frame
    void Update()
    {
        float camDist = (cam.transform.position.x * (1-parallax));  //distance relative to the cam
        float distance = (cam.transform.position.x * parallax); //distance the layer moves relative to the world
        transform.position = new Vector3(start + distance, transform.position.y, transform.position.z); //move the bg along the x axis

        //if the edge of the cam is greater than the edge of the img, move the img over so that it loops
        if (camDist > start + length) {
            start += length;
        }
        else if (camDist < start - length) {
            start -= length;
        }
    }
}
