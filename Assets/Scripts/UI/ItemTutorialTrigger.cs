using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTutorialTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UiObject;
    private bool playerIsNear;

    void Start()
    {
        UiObject.SetActive(false);
    }

/*    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.C))
        {
            ToggleUiObject();
        }
    }*/

    void ToggleUiObject()
    {
        UiObject.SetActive(!UiObject.activeSelf); // Toggles the active state of UiObject
        if (UiObject.activeSelf)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.C))
        {
            ToggleUiObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has entered the trigger zone
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has exited the trigger zone
            playerIsNear = false;
            UiObject.SetActive(false);
        }
    }
}

