using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollisionController : MonoBehaviour
{
    public Material lightOff;
    public Material lightOn;
    public GameObject lightbulb;
    public GeneratePattern generatePattern;

    private MeshRenderer objectRenderer;
    private bool isTurnedOn;
    private bool isNearby = false; // Variable to track if player is near the button
    private UnityEvent onButtonPress;

    void Start()
    {
        objectRenderer = lightbulb.GetComponent<MeshRenderer>();
        isTurnedOn = false;
        onButtonPress = new UnityEvent();
        generatePattern = FindObjectOfType<GeneratePattern>();
    }

    void Update()
    {
        if (generatePattern.lightsReset)
        {
            isTurnedOn = false;
            objectRenderer.material = lightOff; // Ensure the light is turned off if lights are reset
        }

        // Check if the player is nearby and the player presses the interact button
        if (isNearby && Input.GetButtonDown("Interact") && !generatePattern.isCorrect && generatePattern.hasGenerated)
        {
            if (!isTurnedOn)
            {
                objectRenderer.material = lightOn;
                generatePattern.StorePressedButton(lightbulb);
                isTurnedOn = true;
            }
            onButtonPress.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearby = false;
        }
    }
}
