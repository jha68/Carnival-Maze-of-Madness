using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second
    private AudioSource audioSource;
    private GameMaster gm;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotate around the y-axis at the specified speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component from the colliding object
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                /*other.GetComponent<PlayerStats>().isKeyFound = true;*/
                gm.keys += 1;
                print(gm.keys);
                audioSource.Play();
            }
            Destroy(gameObject);
        }
    }
}
