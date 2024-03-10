using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class throwing : MonoBehaviour
{
    public float throwForce = 10f;
    public float upForce =(float) 4f;
    public GameObject objectRigidbody;
    public Transform appleSpawn;
    public Transform cam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Throw();
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
