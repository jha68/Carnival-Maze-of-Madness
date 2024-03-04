using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishStamina : MonoBehaviour
{
    public float boostAmount = 20f; // The amount to boost the player's speed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component from the colliding object
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Start the boost coroutine
                playerController.IncreaseStamina(boostAmount);
            }
            Destroy(gameObject);
        }
    }
}
