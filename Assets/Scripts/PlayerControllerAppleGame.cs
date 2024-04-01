using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControllerAppleGame : MonoBehaviour
{


    // the speed of movement
    public float moveSpeed;

    // Time after the steminer is zero, it does not recover


    // the direction of movement
    private Vector3 moveDirection = Vector3.zero;




    void Update()
    {
        float currentSpeed = moveSpeed;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0f, 0f, horizontalInput) * moveSpeed * Time.deltaTime;

        // if (moveDirection != Vector3.zero)
        // {
        //     // Rotating the character to look in the direction of movement
        //     transform.rotation = Quaternion.LookRotation(moveDirection);
        // }

        // Move
        transform.position += movement;
    }





}
