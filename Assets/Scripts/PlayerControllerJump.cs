using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerJump : MonoBehaviour
{
    // the speed of movement
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    //crouch
    public float crouchSpeed;

    // the direction of movement
    private Vector3 moveDirection = Vector3.zero; 

    //jump
    private Rigidbody rb;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    	jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void Update()
    {
        if (moveDirection != Vector3.zero)
        {
            // Rotating the character to look in the direction of movement
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Move
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
    		rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    		isGrounded = false;
    	} // Mode - Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if(isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        // Set new direction of movement based on input
        moveDirection = new Vector3(input.x, 0, input.y).normalized;
    }

    void OnCollisionStay()
    {
    	isGrounded = true;
    }

    void OnControllerColliderHit (ControllerColliderHit hit){
        switch(hit.gameObject.tag) {
            case "JumpPad":
                rb.AddForce(jump * 50f, ForceMode.Impulse);
                break;
        }
    }

}
