using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public Transform bulletSpawnPoint;


    // the speed of movement
    public float moveSpeed;

    public float runSpeedMultiplier = 1.5f;
    private bool isRunning = false;
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDecreasePerSecond = 20f;
    public float staminaRegenPerSecond = 3f;

    public TextMeshProUGUI staminaText;
    public List<GameObject> bulletImages;

    // Time after the steminer is zero, it does not recover
    public float runDelay = 2f;
    private bool canRun = true;
    private float timeSinceStaminaDepleted;

    // the direction of movement
    private Vector3 moveDirection = Vector3.zero;

    public int maxBullets = 5;
    private int currentBullets;
    public Animator animator;
    //jump
    private Rigidbody rb;
    public Vector3 jump;
    public float jumpForce = 4.0f;
    public bool isGrounded;
    //crouch
    public float crouchSpeed;
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
        currentBullets = maxBullets;
        stamina = maxStamina;
        UpdateStaminaText(); // Update the stamina text at the start
        UpdateBulletsUI(); // Update the bullets text at the start
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void Update()
    {
        if (Input.GetKey(sprintKey) && canRun && stamina > 0)
        {
            isRunning = true;
            stamina -= staminaDecreasePerSecond * Time.deltaTime;
            stamina = Mathf.Max(stamina, 0);
            if (stamina <= 0)
            {
                canRun = false;
                timeSinceStaminaDepleted = 0;
            }
        }
        else
        {
            isRunning = false;
        }
        //jump
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        } // Mode - Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        if (!canRun)
        {
            timeSinceStaminaDepleted += Time.deltaTime;
            if (timeSinceStaminaDepleted >= runDelay)
            {
                canRun = true;
            }
        }

        if (stamina < maxStamina)
        {
            stamina += staminaRegenPerSecond * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        if (stamina <= 0 && isRunning)
        {
            isRunning = false;
            canRun = false;
            timeSinceStaminaDepleted = 0;
        }


        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float speed = input.magnitude;
        float currentSpeed = moveSpeed;
        if (isRunning)
        {
            currentSpeed *= runSpeedMultiplier;
            speed *= runSpeedMultiplier;
        }
        animator.SetFloat("Speed", speed);


        if (moveDirection != Vector3.zero)
        {
            // Rotating the character to look in the direction of movement
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Move
        transform.position += moveDirection * currentSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && currentBullets > 0)
        {
            GetComponent<BulletShooter>().Shoot();
            currentBullets--;
        }
        UpdateStaminaText(); // Update the stamina text every frame
        UpdateBulletsUI(); // Update the bullets text every frame

    }


    void UpdateStaminaText()
    {
        if (staminaText != null) // Check if the Text reference is assigned
        {
            staminaText.text = "Stamina: " + Mathf.RoundToInt(stamina).ToString();
        }
    }

    void UpdateBulletsUI()
    {
        for (int i = 0; i < bulletImages.Count; i++)
        {
            // If the current index is less than the currentBullets, enable the bullet image
            if (i < currentBullets)
            {
                bulletImages[i].SetActive(true);
            }
            else
            {
                bulletImages[i].SetActive(false);
            }
        }
    }


    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        // Set new direction of movement based on input
        moveDirection = new Vector3(input.x, 0, input.y).normalized;
    }

    public void IncreaseStamina(float amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        UpdateStaminaText();
    }

    // increasing the number of bullets
    public void AddBullets(int amount)
    {
        currentBullets += amount;
        currentBullets = Mathf.Clamp(currentBullets, 0, maxBullets); // Ensure that the number of bullets does not exceed the maximum
        UpdateBulletsUI();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "JumpPad":
                rb.AddForce(jump * 50f, ForceMode.Impulse);
                break;
        }
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
}