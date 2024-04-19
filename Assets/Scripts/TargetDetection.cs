using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetDetection : MonoBehaviour
{
    public throwing numTarget;
    public GameObject target11;
    public GameObject target22;
    public GameObject target33;

    void Start() {
        numTarget.numHit = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        float dis = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);

        if (other.tag == "Apple" && dis < 0.5) 
        {
            if(gameObject.tag == "Target1") {
                Debug.Log("hit");
                // Debug.Log(numHit);
                //Destroy(GameObject.FindWithTag("Target1"));
                Destroy(target11);
                numTarget.numHit += 1;
                Debug.Log("Target 1");
            }

            if(gameObject.tag == "Target2") {
                Debug.Log("hit");
                // Debug.Log(numHit);
                //Destroy(GameObject.FindWithTag("Target2"));
                Destroy(target22);
                numTarget.numHit += 1;
                Debug.Log("Target 2");
            }

            if(gameObject.tag == "Target3") {
                Debug.Log("hit");
                // Debug.Log(numHit);
                //Destroy(GameObject.FindWithTag("Target3"));
                Destroy(target33);
                numTarget.numHit += 1;
                Debug.Log("Target 3");
            }
        }
    }

}
