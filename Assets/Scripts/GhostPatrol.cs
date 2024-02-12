using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPatrol : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the ghost
    public float distance = 10.0f; // Distance to move forward and backward

    private Vector3 startPos; // Starting position
    private bool movingForward = true; // Direction of movement

    void Start()
    {
        startPos = transform.position; // Initialize starting position
    }

    void Update()
    {
        // Set the target position based on current direction
        Vector3 targetPos = movingForward ? startPos + transform.forward * distance : startPos;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Check if the ghost has reached the target position
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            // Change direction of movement
            movingForward = !movingForward;

            // Rotate the ghost to face the opposite direction
            transform.Rotate(0, 180f, 0);
        }
    }
}