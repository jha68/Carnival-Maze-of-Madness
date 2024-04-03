using UnityEngine;

public class AppleButton : MonoBehaviour
{
    private SwitchAppleGame switchAppleGameScript;

    private void Start()
    {
        switchAppleGameScript = FindObjectOfType<SwitchAppleGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchAppleGameScript.PlayerEnteredTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switchAppleGameScript.PlayerExitedTrigger();
        }
    }
}

