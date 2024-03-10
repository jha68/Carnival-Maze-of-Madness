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

    private float intervalBetweenLights = 1f;
    private GameObject[] orderedLightbulbs; // the correct pattern
    private GameObject[] pressedLightbulbs; // the player's input pattern
    private UnityEvent onButtonPress;

    public Transform player; // Reference to the player GameObject
    public Camera mainCamera;
    public float rotationSpeed = 5f; // Speed of camera rotation
    public float distanceFromPlayer = 3f; // Distance of camera from player
    public float heightOffset = 0f; // Height offset of camera from player
    private bool rotating = false; // Flag to indicate if rotation is in progress

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
                //if (!rotating)
                //{
                //    RotateCamera();
                //}

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
                FinishMiniGame();
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

    void FinishMiniGame()
    {
        // Restore player's position after finishing mini-game
        float posX = PlayerPrefs.GetFloat("PlayerPosX");
        float posY = PlayerPrefs.GetFloat("PlayerPosY");
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ");

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(posX, posY, posZ);

        SceneManager.LoadScene("SampleScene - Copy");

    }

    //void RotateCamera()
    //{
    //    Vector3 playerForward = player.forward;
    //    Quaternion targetRotation = Quaternion.LookRotation(playerForward, Vector3.up);
    //    StartCoroutine(RotateCameraCoroutine(targetRotation));
    //}

    //IEnumerator RotateCameraCoroutine(Quaternion targetRotation)
    //{
    //    rotating = true;

    //    Smoothly rotate the camera towards the target rotation
    //    while (Quaternion.Angle(mainCamera.transform.rotation, targetRotation) > 0.01f)
    //    {
    //        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //        Calculate new position for the camera behind the player

    //       Vector3 newPosition = player.position - (player.forward * distanceFromPlayer) + (Vector3.up * heightOffset);
    //        mainCamera.transform.position = newPosition;
    //        yield return null;
    //    }

    //    rotating = false;
    //}
}
