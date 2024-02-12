using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPatrol : MonoBehaviour
{
    public float speed = 5.0f;
    public float patrolRadius = 5.0f; // Radius of the patrol circle
    private float patrolAngle = 0.0f; // Current angle of the ghost on the patrol path
    public Transform player;
    public float detectionRange = 15.0f;
    public float chaseRange = 20.0f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && Vector3.Distance(startPos, player.position) <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            CircularPatrol();
        }
    }

    void CircularPatrol()
    {
        // Calculate the next position on the patrol circle based on the current angle
        Vector3 nextPos = startPos + new Vector3(Mathf.Sin(patrolAngle), 0, Mathf.Cos(patrolAngle)) * patrolRadius;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        // Look towards the moving direction
        RotateTowards(nextPos);

        // Increment the patrol angle to move to the next position on the next frame
        patrolAngle += speed * Time.deltaTime / patrolRadius;

        // Optional: Wrap the angle to prevent overflow
        if (patrolAngle > 2 * Mathf.PI)
        {
            patrolAngle -= 2 * Mathf.PI;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        if (directionToTarget != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
