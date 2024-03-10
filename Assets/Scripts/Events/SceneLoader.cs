using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    private Vector3 playerPosition;
    private UnityEvent onButtonPress = new UnityEvent();

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact"))
            {
                LoadMiniGame();
                onButtonPress.Invoke();
            }
        }
    }

    void LoadMiniGame()
    {
        // Save player's position before loading mini-game scene
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

        SceneManager.LoadScene("ChloeScene");
    }

}
