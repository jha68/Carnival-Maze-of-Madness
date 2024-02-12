using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGateController : MonoBehaviour
{
    public Animator right; 

   

    private void OnTriggerEnter(Collider other)
    {
        print("Player is near");
        if (other.gameObject.tag == "Player")
        {
            right.SetBool("Near", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        print("Player is not near");
        if (other.gameObject.tag == "Player")
        {
            right.SetBool("Near", false);
        }
    }
}
