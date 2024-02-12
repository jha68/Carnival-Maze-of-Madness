using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject found by tag
    private Vector3 offset; // Initial offset from the player

    public float distance = 10.0f; // Distance between the camera and the character
    public float height = 5.0f; // Height of the camera relative to the character
    public float cameraSpeed = 10.0f; // Speed at which the camera follows the character

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindWithTag("Player");

        // Calculate initial offset if player is found
        if (player != null)
        {
            // Set the initial offset to keep the camera at the start position
            offset = transform.position - player.transform.position;
        }
    }

    void LateUpdate()
    {
        if (player == null) return; // Check if the player has been found

        // Calculate the desired position based on the player's position and the initial offset
        Vector3 desiredPosition = player.transform.position + offset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraSpeed);
    }
}
