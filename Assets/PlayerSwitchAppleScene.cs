using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSwitchAppleScene : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;

        }

        if (isPaused) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("throwingApples");
            Time.timeScale = 1f;
        }
    }
}
