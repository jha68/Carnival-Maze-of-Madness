using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class throwing : MonoBehaviour
{
    public float throwForce = 10f;
    public float upForce =(float) 4f;
    public GameObject objectRigidbody;
    public Transform appleSpawn;
    public Transform cam;
    public int numHit;

    // void Start() {
    //     numHit = 0;
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Throw();
        }

        if (numHit == 3) {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1f;
        }
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
