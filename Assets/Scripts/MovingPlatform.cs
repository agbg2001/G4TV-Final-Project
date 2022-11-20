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
    private int currentWaypointIndex = 0;
    public bool isActive = false;

    void Start()
    {
        targetWaypoint = waypoints[0];
        rBody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (isActive)
        {
            StartCoroutine(Move(gameObject, targetWaypoint.position, speed));
        }

        if (Vector2.Distance(transform.position, targetWaypoint.position) < checkDistance)
        {
            targetWaypoint = GetNextWaypoint();
        }
    }

    public IEnumerator Move(GameObject obj, Vector2 target, float speed)
    {
        Vector2 startPosition = obj.transform.position;
        float time = 0f;

        while (rBody.position != target)
        {
            obj.transform.position = Vector2.MoveTowards(startPosition, target, (time / Vector2.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.transform.SetParent(gameObject.transform, true);
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

