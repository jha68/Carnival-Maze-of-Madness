using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerSwitchAppleScene : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isMiniGameActive = false;

    private EventSystem currentEventSystem;
    private AudioListener currentAudioListener;

    void Awake()
    {
        currentEventSystem = FindObjectOfType<EventSystem>();
        currentAudioListener = FindObjectOfType<AudioListener>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused || isMiniGameActive) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (currentEventSystem) currentEventSystem.gameObject.SetActive(false);
            if (currentAudioListener) currentAudioListener.enabled = false;
            SceneManager.LoadSceneAsync("throwingApples", LoadSceneMode.Additive);
            isMiniGameActive = true;
        }

    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene") 
        {
            var playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }
            if (currentEventSystem) currentEventSystem.gameObject.SetActive(true);
            if (currentAudioListener) currentAudioListener.enabled = true;
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "throwingApples")
        {
            isMiniGameActive = false;
        }
    }
}
