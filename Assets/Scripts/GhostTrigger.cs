using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    private HealthBarHUDTester healthBarHUDTester;

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
    }
}

