using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EscapeTime : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveDistance = 10f;

    public GameObject playerReplica; // Reference to the child object (player replica)

    private bool isMoving = false;
    public ActivateWin activatWinScript;

    private void Start()
    {
        // Optionally make sure the player replica is not visible at the start
        if (playerReplica != null)
        {
            playerReplica.SetActive(false);
        }

        activatWinScript = FindObjectOfType<ActivateWin>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            other.gameObject.SetActive(false); // Disable the actual player

            // Enable the player replica
            if (playerReplica != null)
            {
                playerReplica.SetActive(true);
            }

            StartCoroutine(MoveUpward());
        }
    }

    IEnumerator MoveUpward()
    {
        isMoving = true;
        activatWinScript.active = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z);

        float elapsedTime = 0;
        while (elapsedTime < moveDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / (moveDistance / moveSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isMoving = false;
    }
}
