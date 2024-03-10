using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetDetection : MonoBehaviour
{
    float numHit;

    private void Start()
    {
        numHit = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // help
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        float dis = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);

        if (other.tag == "Apple" && dis < 0.5) 
        {
            if(gameObject.tag == "Target1") {
                Debug.Log("hit");
                Destroy(GameObject.FindWithTag("Target1"));
                numHit+=1;
                Debug.Log(numHit);
            }

            if(gameObject.tag == "Target2") {
                Debug.Log("hit");
                Destroy(GameObject.FindWithTag("Target2"));
                numHit+=1;
                Debug.Log(numHit);
            }

            if(gameObject.tag == "Target3") {
                Debug.Log("hit");
                Destroy(GameObject.FindWithTag("Target3"));
                numHit+=1;
                Debug.Log(numHit);
            }
        }
    }

}
