using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGateController : MonoBehaviour
{
    public Animator left; 

   

    private void OnTriggerEnter(Collider other)
    {
        print("Player is near");
        if (other.gameObject.tag == "Player")
        {
            left.SetBool("Near", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        print("Player is not near");
        if (other.gameObject.tag == "Player")
        {
            left.SetBool("Near", false);
        }
    }
}
