using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] public float speed = 10;
    [SerializeField] private float checkDistance = 0.05f;
    private Rigidbody2D rBody;
    private Transform targetWaypoint;
    private Transform originWaypoint;
    private int currentWaypointIndex = 0;
    public bool isActive = false;

    void Start()
    {
        targetWaypoint = waypoints[0];
        originWaypoint = waypoints[waypoints.Length-1];
        rBody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if (isActive)
        {
            StartCoroutine(Move(gameObject, originWaypoint.position, targetWaypoint.position, speed));
        }

        if (Vector2.Distance(transform.position, targetWaypoint.position) < checkDistance)
        {
            originWaypoint = targetWaypoint;
            targetWaypoint = GetNextWaypoint();
        }
    }

    public IEnumerator Move(GameObject obj, Vector2 origin, Vector2 target, float speed)
    {
        Vector2 startPosition = obj.transform.position;
        float time = 0f;

        while (rBody.position != target)
        {
            obj.transform.position = Vector2.MoveTowards(startPosition, target, (time / Vector2.Distance(origin, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //col.gameObject.transform.SetParent(gameObject.transform, true);
        GameObject player = col.gameObject;
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        
        /*Debug.DrawRay(player.transform.position + Vector3.right * playerCollider.size.x/2.5f, Vector2.down * 2, Color.green);   //one ray on the right side of the collider
        Debug.DrawRay(player.transform.position + Vector3.left * playerCollider.size.x/2.5f, Vector2.down * 2, Color.green);    //other ray on left side*/

        LayerMask movePlatform = LayerMask.GetMask("MovePlatform");

        RaycastHit2D hitMove = Physics2D.Raycast(player.transform.position + Vector3.right * playerCollider.size.x/2.5f, -Vector2.up, 0.1f, movePlatform);
        RaycastHit2D hit2Move = Physics2D.Raycast(player.transform.position + Vector3.left * playerCollider.size.x/2.5f, -Vector2.up, 0.1f, movePlatform);

        if (hitMove.collider != null || hit2Move.collider != null) { //if the rays are touching the moving platforms
            col.gameObject.transform.SetParent(gameObject.transform, true);
        }
         else { 
            col.gameObject.transform.parent = null;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.parent = null;
    }

    private Transform GetNextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
        return waypoints[currentWaypointIndex];
    }

}

