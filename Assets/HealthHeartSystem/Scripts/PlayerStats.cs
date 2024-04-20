/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;
    [SerializeField]
    private Vector3 respawnPosition; // The position where the player should respawn
    [SerializeField]
    private float respawnHealth = 3f; // The health value the player respawns with

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }
    public bool isCatFound = false;
/*    public bool isKeyFound = false;*/
    private AudioSource audioSource;
    private GameMaster gm;

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();
        if (health <= 0)
        {
            Respawn();
        }
        if (audioSource != null)
        {
            audioSource.Play(); // Play the damage sound
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
    void Respawn()
    {
        transform.position = gm.lastCheckPointPos; // Move the player to the respawn position
        transform.rotation = Quaternion.identity; // Reset the player's rotation to default (no rotation)
        health = respawnHealth; // Restore health to respawn value
        isCatFound = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Reset linear velocity
            rb.angularVelocity = Vector3.zero; // Reset angular velocity
        }
        PlayerStats.Instance.Heal(10f);
    }
}
