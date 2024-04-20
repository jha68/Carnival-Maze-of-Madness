using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float minimumDistanceToPlayer = 0.5f; // The minimum distance to keep from the player
    public bool isFound = false;
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to this object
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isFound)
        {
            ChasePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFound = true;
            // Set the isPetFound flag on the PlayerStats instance to true
            other.GetComponent<PlayerStats>().isCatFound = true;
        }
    }


    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 targetPosition = player.position - directionToPlayer * minimumDistanceToPlayer;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        if (distanceToPlayer < minimumDistanceToPlayer + 0.35f)
        {
            animator.SetBool("isWalking", false);
        }
        else if (distanceToPlayer > minimumDistanceToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
    }
}