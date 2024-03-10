using UnityEngine;
 
public class JumpPad : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject block = collision.gameObject;
        Rigidbody rb = block.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 50);
    }
}

