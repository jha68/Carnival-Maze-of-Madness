using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UiObject;
    void Start()
    {
        UiObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            UiObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        UiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

