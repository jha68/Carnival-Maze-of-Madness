using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControllerAppleGame : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 move;

    void Update()
    {
        float currentSpeed = moveSpeed;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0f, 0f, horizontalInput) * moveSpeed * Time.deltaTime;
        move = movement;

        // Move
        transform.position += movement;
    }
}
