using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healAmount = 1f; // Amount of health to restore
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a PlayerStats component attached
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}

