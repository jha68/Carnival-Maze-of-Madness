using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPickup : MonoBehaviour
{
    public int ammoAmount = 1; // Number of bullets to be added to the player if this item is picked up

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Allows players to pick up bullets
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.AddBullets(ammoAmount);
                Destroy(gameObject); // Remove bullet items from game
            }
        }
    }
}
