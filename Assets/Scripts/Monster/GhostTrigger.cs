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
            healthBarHUDTester?.Hurt(1f);
        }

        if (other.gameObject.CompareTag("Bullet")) 
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

