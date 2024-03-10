using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollisionController: MonoBehaviour
{
    public Material lightOff;
    public Material lightOn;
    public GameObject lightbulb;
    public GeneratePattern generatePattern;

    private MeshRenderer objectRenderer;
    private bool isTurnedOn;
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
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            if (Input.GetButtonDown("Interact") && !generatePattern.isCorrect && generatePattern.hasGenerated)
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
    }

}
