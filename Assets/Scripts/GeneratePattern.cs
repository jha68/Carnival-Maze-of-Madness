using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GeneratePattern : MonoBehaviour
{

    public Material lightOff;
    public Material lightOn;
    public Material correct;
    public Material wrong;

    public bool hasGenerated;
    public bool isCorrect;
    public bool lightsReset;

    private float intervalBetweenLights = 1f;
    private GameObject[] orderedLightbulbs; // the correct pattern
    private GameObject[] pressedLightbulbs; // the player's input pattern
    private UnityEvent onButtonPress;

    // Start is called before the first frame update
    void Start()
    {
        orderedLightbulbs = new GameObject[4];
        pressedLightbulbs = new GameObject[4];

        orderedLightbulbs[0] = GameObject.Find("LightBulb");
        orderedLightbulbs[1] = GameObject.Find("LightBulb (2)");
        orderedLightbulbs[2] = GameObject.Find("LightBulb (3)");
        orderedLightbulbs[3] = GameObject.Find("LightBulb (1)");

        hasGenerated = false;
        isCorrect = false;
        lightsReset = false;
        onButtonPress = new UnityEvent();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with button.");

            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("Button pressed!");
                onButtonPress.Invoke();

                if (!hasGenerated && !isCorrect)
                {
                    StartCoroutine(PlayPattern());
                    hasGenerated = true;
                    lightsReset = false;
                    Debug.Log("pattern generated");
                }

            }
        }
    }

    public void StorePressedButton(GameObject lightbulb)
    {
        for (int i = 0; i < pressedLightbulbs.Length; i++)
        {
            if (pressedLightbulbs[i] == null)
            {
                pressedLightbulbs[i] = lightbulb;

                if (i == pressedLightbulbs.Length - 1) // once the last button was pressed, check pattern
                {
                    Debug.Log("pattern checked");
                    CheckPattern();
                }

                break; // Exit loop after adding the button
            }  
        }
    }

    private IEnumerator PlayPattern()
    {
        for (int i = 0; i < orderedLightbulbs.Length; i++)
        {
            orderedLightbulbs[i].GetComponent<MeshRenderer>().material = lightOn;

             // Wait for a certain amount of time
            yield return new WaitForSeconds(intervalBetweenLights);

            orderedLightbulbs[i].GetComponent<MeshRenderer>().material = lightOff;
        }
    }

    private void ResetLights()
    {
        for (int i = 0; i < orderedLightbulbs.Length; i++)
        {
            orderedLightbulbs[i].GetComponent<MeshRenderer>().material = lightOff;
        }

        lightsReset = true;
    }

    private void CheckPattern()
    {
        for (int i = 0; i < pressedLightbulbs.Length; i++)
        {
            if (pressedLightbulbs[i] != orderedLightbulbs[i])
            {
                pressedLightbulbs = new GameObject[4];
                Debug.Log("wrong pattern!");
                StartCoroutine(DisplayWrong());
                hasGenerated = false;
                break;
            }

            if (i == pressedLightbulbs.Length - 1)
            {
                Debug.Log("correct pattern!");
                isCorrect = true;
                DisplayCorrect();
            }
        }
    }

    private void DisplayCorrect()
    {
        for (int i = 0; i < orderedLightbulbs.Length; i++)
        {
            orderedLightbulbs[i].GetComponent<MeshRenderer>().material = correct;
        }

    }

    private IEnumerator DisplayWrong()
    {
        for (int i = 0; i < orderedLightbulbs.Length; i++)
        {
            orderedLightbulbs[i].GetComponent<MeshRenderer>().material = wrong;
        }

        float errorInterval = 2f;

        // Wait for a certain amount of time
        yield return new WaitForSeconds(errorInterval);

        ResetLights();
    }

}
