using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    public float boostAmount = 2.0f; // The amount to boost the player's speed
    public float boostDuration = 0.1f; // How long the boost lasts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component from the colliding object
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Start the boost coroutine
                //StartCoroutine(playerController.BoostSpeed(boostAmount, boostDuration));
            }
            Destroy(gameObject);
        }
    }
}
