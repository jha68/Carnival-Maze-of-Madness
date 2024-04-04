using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

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
    private bool allowJump = false;

    //crouch
    public float crouchSpeed;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    private HealthBarHUDTester healthBarHUDTester;
    private float airTime = 0f;
    private bool triggerFalling = false;
    public static bool isPaused = false;


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
        healthBarHUDTester = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBarHUDTester>();
        animator = GetComponent<Animator>();
    }
    private bool isFalling = false;
    private float fallStartLevel;
    public float fallDamageThreshold = 1f; // The height threshold to start taking damage from falling
    public float damageMultiplier = .1f; // Adjusts the amount of damage taken from falling

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;

        }

        if (isPaused || SceneManager.GetActiveScene().name == "throwingApples") return;

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

        if (allowJump)
        {
            //jump
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                airTime = 0f;
                triggerFalling = false;
                animator.SetBool("isJumping", true);
            } // Mode - Crouching
              //else if (Input.GetKey(crouchKey))
              //{
              //    state = MovementState.crouching;
              //    moveSpeed = crouchSpeed;
              //}

            if (!isGrounded)
            {
                airTime += Time.deltaTime;
                // Check to trigger the falling animation after being in the air for more than 1.5 seconds
                if (airTime > 1.5f && !triggerFalling)
                {
                    Debug.Log("It's falling");
                    animator.SetBool("isFalling", true); // Trigger falling animation
                    triggerFalling = true; // Ensure we don't repeatedly trigger the falling animation
                }
            }
            else
            {
                // Reset conditions when the player lands
                if (triggerFalling)
                {
                    animator.SetBool("isFalling", false); // Stop falling animation
                    triggerFalling = false; // Reset for next jump
                }
                animator.SetBool("isJumping", false); // Ensure jumping is reset when grounded
            }
            //end jump
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
        if (!SwitchAppleGame.isMiniGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Z) && currentBullets > 0)
            {
                GetComponent<BulletShooter>().Shoot();
                currentBullets--;
            }
        }
            
        UpdateStaminaText(); // Update the stamina text every frame
        UpdateBulletsUI(); // Update the bullets text every frame
        // Check if the player has started falling
        if (!isGrounded && !isFalling)
        {
            isFalling = true;
            fallStartLevel = transform.position.y;
        }

        // Check if the player has landed
        if (isGrounded && isFalling)
        {
            isFalling = false;
            float fallDistance = fallStartLevel - transform.position.y;
            if (fallDistance > fallDamageThreshold)
            {
                // Apply damage based on fall distance minus the threshold
                float damage = (fallDistance - fallDamageThreshold) * damageMultiplier;
                healthBarHUDTester?.Hurt(damage);
                Debug.Log(damage);
            }
        }

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
        animator.SetBool("isJumping", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ParkourArea"))
        {
            allowJump = true;
            print("can jump now");
            print(allowJump);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ParkourArea"))
        {
            allowJump = false;
            print("cannot jump now");
            print(allowJump);
        }
    }
}