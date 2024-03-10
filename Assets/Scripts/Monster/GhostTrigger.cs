using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    private HealthBarHUDTester healthBarHUDTester;

    public int health = 2;
    private void Start()
    {
        // Assuming the HealthBarHUDTester script is on the player GameObject
        healthBarHUDTester = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBarHUDTester>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the GhostPatrol component to check if the ghost is currently stunned
            GhostPatrol ghostPatrol = GetComponent<GhostPatrol>();
            if (ghostPatrol != null && !ghostPatrol.isStunned)
            {
                // Only hurt the player if the ghost is not stunned
                healthBarHUDTester?.Hurt(1f);
            }
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            //TakeDamage(1); // Assuming you have a method to decrease the ghost's health
            Destroy(other.gameObject); // Destroy the bullet
            GhostPatrol ghostPatrol = GetComponent<GhostPatrol>();
            if (ghostPatrol != null)
            {
                ghostPatrol.GetStunned(2.0f); // Stun the ghost for 2 seconds
            }
        }
    }


    // void TakeDamage(int damage)
    // {
    //     health -= damage;
    //     if (health <= 0)
    //     {
    //         Die(); 
    //     }
    // }

    // void Die()
    // {
    //     Destroy(gameObject);
    // }
}

