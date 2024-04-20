using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGateController : MonoBehaviour
{
    public Animator right;
    private GameMaster gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null && playerStats.isCatFound && gm.keys == 1)
            {
                right.SetBool("Near", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            right.SetBool("Near", false);
        }
    }
}
