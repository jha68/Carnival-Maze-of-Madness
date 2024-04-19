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
    public float intervalBetweenOnOff = .5f; // Interval for which the light is on

    private MeshRenderer objectRenderer;
    private bool isTurnedOn;
    private bool isNearby = false; // Variable to track if player is near the button
    private UnityEvent onButtonPress;

    private AudioSource audioSource;

    void Start()
    {
        objectRenderer = lightbulb.GetComponent<MeshRenderer>();
        isTurnedOn = false;
        onButtonPress = new UnityEvent();
        generatePattern = FindObjectOfType<GeneratePattern>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            if (!isTurnedOn)
            {
                StartCoroutine(TurnOnAndOff()); // Start the coroutine to turn on and off the light
                generatePattern.StorePressedButton(lightbulb);
            }
            onButtonPress.Invoke();
        }
    }

    private IEnumerator TurnOnAndOff()
    {
        objectRenderer.material = lightOn; // Turn on the light
        isTurnedOn = true;

        yield return new WaitForSeconds(intervalBetweenOnOff); // Wait for the interval

        if (!generatePattern.playerSubmitted) {
            objectRenderer.material = lightOff; // Turn off the light
            isTurnedOn = false;
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
