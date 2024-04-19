using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GeneratePattern : MonoBehaviour
{

    public Material lightOff;
    public Material lightOn;
    public Material correct;
    public Material wrong;

    public bool hasGenerated;
    public bool isCorrect;
    public bool lightsReset;
    public bool playerSubmitted;

    private float intervalBetweenLights = 1.5f;
    private GameObject[] lightBulbs; // the correct pattern
    private GameObject[] pressedLightbulbs; // the player's input pattern
    private GameObject[] pattern;

    private UnityEvent onButtonPress;
    private AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        lightBulbs = new GameObject[4];
        pressedLightbulbs = new GameObject[6];
        pattern = new GameObject[6];

        lightBulbs[0] = GameObject.Find("LightBulb");
        lightBulbs[1] = GameObject.Find("LightBulb (1)");
        lightBulbs[2] = GameObject.Find("LightBulb (2)");
        lightBulbs[3] = GameObject.Find("LightBulb (3)");

        pattern[0] = GameObject.Find("LightBulb");
        pattern[1] = GameObject.Find("LightBulb (1)");
        pattern[2] = GameObject.Find("LightBulb (2)");
        pattern[3] = GameObject.Find("LightBulb (3)");
        pattern[4] = GameObject.Find("LightBulb");
        pattern[5] = GameObject.Find("LightBulb (1)");

        hasGenerated = false;
        isCorrect = false;  
        onButtonPress = new UnityEvent();
        playerSubmitted = false;

        audioSource = GetComponent<AudioSource>();
    }

    private void ShufflePattern()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            int randomIndex = Random.Range(0, pattern.Length);
            GameObject temp = pattern[i];
            pattern[i] = pattern[randomIndex];
            pattern[randomIndex] = temp;
        }
    }

    private bool playerIsNear = false;

    public void PlayerEnteredTrigger()
    {
        playerIsNear = true;
    }

    public void PlayerExitedTrigger()
    {
        playerIsNear = false;
    }

    private void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.C) && !isCorrect && !hasGenerated)
        {
            audioSource.Play();
            ShufflePattern();
            StartCoroutine(PlayPattern());
            hasGenerated = true;
            lightsReset = false;
            Debug.Log("pattern generated");
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
                    playerSubmitted = true;
                    CheckPattern();
                }

                break; // Exit loop after adding the button
            }
        }
    }

    private IEnumerator PlayPattern()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i].GetComponent<MeshRenderer>().material = lightOn;

            // Wait for a certain amount of time
            yield return new WaitForSeconds(intervalBetweenLights / 2);

            pattern[i].GetComponent<MeshRenderer>().material = lightOff;

            yield return new WaitForSeconds(intervalBetweenLights / 2);

        }
    }

    private void ResetLights()
    {
        for (int i = 0; i < lightBulbs.Length; i++)
        {
            lightBulbs[i].GetComponent<MeshRenderer>().material = lightOff;
        }

        ShufflePattern();
        lightsReset = true;
    }

    private void CheckPattern()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pressedLightbulbs[i] != pattern[i])
            {
                pressedLightbulbs = new GameObject[6];
                Debug.Log("wrong pattern!");
                StartCoroutine(DisplayWrong());
                hasGenerated = false;
                playerSubmitted = false;
                return;
            }
        }

        Debug.Log("correct pattern!");
        isCorrect = true;
        DisplayCorrect();
        OpenGate();
    }

    private void DisplayCorrect()
    {
        for (int i = 0; i < lightBulbs.Length; i++)
        {
            lightBulbs[i].GetComponent<MeshRenderer>().material = correct;
        }

    }

    private IEnumerator DisplayWrong()
    {
        for (int i = 0; i < lightBulbs.Length; i++)
        {
            lightBulbs[i].GetComponent<MeshRenderer>().material = wrong;
        }

        float errorInterval = 2f;

        // Wait for a certain amount of time
        yield return new WaitForSeconds(errorInterval);

        ResetLights();
    }

    public GateOpen gateController;

    void OpenGate()
    {
        gateController.OpenTheGate();
    }

}
