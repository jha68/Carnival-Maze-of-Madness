using UnityEngine;

public class PatternStartTrigger : MonoBehaviour
{
    private GeneratePattern generatePatternScript;

    private void Start()
    {
        // Find the GeneratePattern script in the scene (assuming there's only one)
        // You could also drag and drop to assign this through the inspector
        generatePatternScript = FindObjectOfType<GeneratePattern>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the trigger zone
            generatePatternScript.PlayerEnteredTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the trigger zone
            generatePatternScript.PlayerExitedTrigger();
        }
    }
}
