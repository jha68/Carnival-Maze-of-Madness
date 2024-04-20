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
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (other.tag == "Player") {
            
            if (gameObject.name == "Aid1")
            {
                if (!playerStats.isCatFound)
                {
                    UiObject.SetActive(true);
                }
            } else
            {
                UiObject.SetActive(true);
            }
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

