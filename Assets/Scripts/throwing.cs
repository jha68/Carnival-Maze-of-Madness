using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class throwing : MonoBehaviour
{
    public float throwForce = 10f;
    public float upForce =(float) 4f;
    public GameObject objectRigidbody;
    public Transform appleSpawn;
    public Transform cam;
    public int numHit;

    private GameObject key2;

    void Start() {
        numHit = 0;
        key2 = GameObject.Find("Key2");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Throw();
        }

        if (numHit == 3) {
            key2.SetActive(true);
            StartCoroutine(ReturnToMainScene());
            Time.timeScale = 1f;
        }
    }

    IEnumerator ReturnToMainScene()
    {
        yield return SceneManager.UnloadSceneAsync("throwingApples");
        Camera.main.GetComponent<AudioListener>().enabled = true;
        Debug.Log("hi");
    }

    void Throw()
    {
        // make apple
        GameObject apple = Instantiate(objectRigidbody, appleSpawn.position, Quaternion.identity);
        Rigidbody appleRb = apple.GetComponent<Rigidbody>();
        
        Vector3 force = (cam.transform.forward * throwForce) + (transform.up * upForce);
        appleRb.AddForce(force, ForceMode.Impulse);
    }
}
