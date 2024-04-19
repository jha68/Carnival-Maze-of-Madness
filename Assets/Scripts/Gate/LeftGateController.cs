using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGateController : MonoBehaviour
{
    public Animator left;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null && playerStats.isCatFound && playerStats.isKeyFound)
            {
                left.SetBool("Near", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            left.SetBool("Near", false);
        }
    }
}
