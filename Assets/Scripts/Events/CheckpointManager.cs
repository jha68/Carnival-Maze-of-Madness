using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (name == "Checkpoint1" && gm.lastCheckPoint == 0)
            { 
                gm.lastCheckPoint = 1;
                gm.lastCheckPointPos = transform.position;

            }
            else if (name == "Checkpoint2" && gm.lastCheckPoint == 1)
            {
                gm.lastCheckPoint = 2;
                gm.lastCheckPointPos = transform.position;

            }
            else if (name == "Checkpoint3" && gm.lastCheckPoint == 2)
            {
                gm.lastCheckPoint = 3;
                gm.lastCheckPointPos = transform.position;

            }
        }
    }
/*    public Transform player; // Reference to the player's transform
    private Transform currentCheckpoint; // Store the current active checkpoint

    // Function to set the current checkpoint
    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    // Function to respawn the player at the last activated checkpoint
    public void RespawnPlayer()
    {
        if (currentCheckpoint != null)
        {
            player.position = currentCheckpoint.position; // Move the player to the checkpoint position
            // You might also want to reset player health, energy, etc. here if applicable
        }
        else
        {
            // If no checkpoint is set, respawn the player at the starting position or default respawn location
            Debug.LogWarning("No checkpoint set. Respawning at default location.");
            // Implement default respawn behavior here
        }
    }*/
}

