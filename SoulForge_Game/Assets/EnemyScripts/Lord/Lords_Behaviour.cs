using UnityEngine;
using SoulForge;
using System;

public class Lords_Behaviour : MonoBehaviour, IEnemy
{

    public Animator animator; // Reference to the Animator component
    public LayerMask playerLayer; // Layer mask to identify player layers
    public Transform player; // Reference to the player transform
    public Transform attackPointQuick; // Point from where the attack originates
    public Transform attackPointHeavy; // Point from where the heavy attack originates
    public bool isFlipped = false; // Flag to check if the enemy is flipped

    private int maxHealth = 2000;
    public int currentHealth;
    public int quickDamage = 20; // Damage dealt by the quick attack
    public int heavyDamage = 50; // Damage dealt by the heavy attack
    private float attackRangeQuick = 1f; // Range of the quick attack
    private float attackRangeHeavy = 3f; // Range of the heavy attack
    public bool lordDefeated = false; // Flag to check if the Lord of Darkness is defeated

    private bool isImmune = false; // Flag for immunity frames
    private float immunityDuration = 1.5f; // Duration of immunity in seconds
    private float immunityTimer = 0f; // Timer for immunity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Handle immunity timer
        if (isImmune)
        {
            immunityTimer -= Time.deltaTime;
            if (immunityTimer <= 0f)
            {
                isImmune = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // If immune, ignore damage
        if (isImmune)
            return;

        currentHealth -= damage;
        animator.SetTrigger("Hit"); // Play Hit Animation

        // Activate immunity frames
        isImmune = true;
        immunityTimer = immunityDuration;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(String attackType)
    {
        // Quick attack logic
        if (attackType == "Quick")
        {
            Debug.Log("Lord of Darkness attacks with: " + attackType);
            // Check for players in the quick attack range, using the playerLayer mask
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPointQuick.position, attackRangeQuick, playerLayer);
            // Damage all players in range
            foreach (Collider2D player in hitPlayers)
            {
                IPlayer playerComponent = player.GetComponent<IPlayer>();
                playerComponent?.TakeDamage(quickDamage); // Quick attack damage
            }
        }
        // Heavy attack logic
        else if (attackType == "Heavy")
        {
            Debug.Log("Lord of Darkness attacks with: " + attackType);
            // Check for players in the heavy attack range, using the playerLayer mask
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPointHeavy.position, attackRangeHeavy, playerLayer);
            // Damage all players in range
            foreach (Collider2D player in hitPlayers)
            {
                IPlayer playerComponent = player.GetComponent<IPlayer>();
                if (playerComponent != null)
                {
                    playerComponent.TakeDamage(heavyDamage); // Heavy attack damage
                    lifeSteal(heavyDamage); // Apply life steal based on heavy attack damage
                }
            }
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ensure health does not exceed maximum
        }
    }

    public void lifeSteal(int damage)
    {
        // Calculate life steal based on damage dealt
        int lifeStealAmount = (int)(damage * 0.1f); // Example: 10% of damage dealt
        Heal(lifeStealAmount);
    }

    void win()
    {
        Debug.Log("Lord of Darkness has been defeated! You win!");
        lordDefeated = true; // Set the flag to true when the Lord of Darkness is defeated
                             // Here you can add code to handle the win condition, like loading a new scene or showing a victory message
    }

    void Die()
    {
        animator.SetBool("IsDead", true); // Set the IsDead parameter to true in the Animator

        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
        this.enabled = false; // Disable the script to stop further updates
    }

    public void LookAtPlayer()
    {
        // Determine direction to player
        float direction = player.position.x - transform.position.x;
        if (direction != 0)
        {
            // Invert the direction for flipping if your sprite faces left by default
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Sign(direction) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the attack range
        if (attackPointQuick && attackPointHeavy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPointQuick.position, 1.3f);
            Gizmos.DrawWireSphere(attackPointHeavy.position, 2.7f);
        }
    }
}
