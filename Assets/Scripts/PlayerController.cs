using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // the speed of movement
    public float moveSpeed;

    // the direction of movement
    private Vector3 moveDirection = Vector3.zero; 

    void Update()
    {
        if (moveDirection != Vector3.zero)
        {
            // Rotating the character to look in the direction of movement
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Move
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        // Set new direction of movement based on input
        moveDirection = new Vector3(input.x, 0, input.y).normalized;
    }
}