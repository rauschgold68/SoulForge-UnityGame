using UnityEngine;
using SoulForge;

public class Golem_Behaviour : MonoBehaviour, IEnemy
{
    public Animator animator; // Reference to the Animator component

    public Transform attackPointQuick; // Point from where the quick attack originates
    public Transform attackPointHeavy; // Point from where the heavy attack originates
    public LayerMask playerLayer; // Layer mask to identify player layers

    public int maxHealth = 200;
    public int currentHealth;
    public int quickDamage = 15; // Damage dealt by the quick attack
    public int heavyDamage = 35; // Damage dealt by the heavy attack
    private float attackRangeQuick = 1f; // Range of the quick attack
    private float attackRangeHeavy = 2f; // Range of the heavy attack

    private bool isImmune = false; // True if Golem is currently immune to damage
    private float immunityDuration = 1.8f; // Duration of immunity in seconds
    private float immunityTimer = 0f; // Timer for tracking immunity
    private bool isHealing = false; // True if Golem is currently healing

    public Vector3 starterScale;

    public bool isChasing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        starterScale = transform.localScale;
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
                isHealing = false;
                // Optionally reset visual cue here
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isImmune) return; // Ignore damage if immune
        currentHealth -= damage;
        animator.SetTrigger("Hit"); // Play Hit Animation
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            isImmune = true;
            // Randomize immunity duration
            float rand = Random.value;
            if (rand < 0.1f)
            {
                // 10% chance: very short immunity (0–0.2s)
                immunityTimer = Random.Range(0f, 0.2f);
            }
            else if (rand < 0.7f)
            {
                // 60% chance: medium immunity (40–80% of default)
                immunityTimer = Random.Range(immunityDuration * 0.4f, immunityDuration * 0.8f);
            }
            else
            {
                // 30% chance: full default immunity
                immunityTimer = immunityDuration;
            }
            // Optionally trigger visual cue here
        }
    }

    /// <summary>
    /// Executes an attack action based on the given type (Quick or Heavy).
    /// </summary>
    /// <param name="attackType">Type of action: "Quick" or "Heavy"</param>
    public void Attack(string attackType)
    {
        if (attackType == "Quick" && attackPointQuick != null)
        {
            // Quick attack logic
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPointQuick.position, attackRangeQuick, playerLayer);
            foreach (Collider2D player in hitPlayers)
            {
                IPlayer playerComponent = player.GetComponent<IPlayer>();
                playerComponent?.TakeDamage(quickDamage);
            }
        }
        else if (attackType == "Heavy" && attackPointHeavy != null)
        {
            // Heavy attack logic
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPointHeavy.position, attackRangeHeavy, playerLayer);
            foreach (Collider2D player in hitPlayers)
            {
                IPlayer playerComponent = player.GetComponent<IPlayer>();
                playerComponent?.TakeDamage(heavyDamage);
            }
        }
    }

    /// <summary>
    /// Instantly triggers the Golem's healing action, applying immunity and healing.
    /// </summary>
    public void TriggerStoneRegen()
    {
        if (!isImmune && !isHealing) // Only heal if not already immune or healing
        {
            isImmune = true;
            isHealing = true;
            immunityTimer = immunityDuration; // Golem is immune during healing
        }
        Heal(15); // Heal for 15 HP
    }

    /// <summary>
    /// Heals the golem by the specified amount, without exceeding maxHealth.
    /// </summary>
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    /// <summary>
    /// Handles golem death logic.
    /// </summary>
    void Die()
    {
        Debug.Log("Golem has died.");
        animator.SetBool("IsDead", true); // Set the IsDead parameter to true in the Animator
        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
    }

    public void Revive()
    {
        Debug.Log("Golem has revived.");
        currentHealth = maxHealth; // Reset health to max
        animator.SetBool("IsDead", false); // Reset the IsDead parameter in the Animator
        GetComponent<Collider2D>().enabled = true; // Re-enable the collider
        isImmune = false; // Reset immunity state
        isHealing = false; // Reset healing state
        isChasing = false; // Reset aggro state
        // Stop all movement
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
        // Flip to starter side
        if (starterScale != Vector3.zero) transform.localScale = starterScale;
    }

    // Utility method for state machine to check if Golem is immune
    public bool IsImmune() { return isImmune; }

    public void LookAtPlayer(Transform player)
    {
        float direction = player.position.x - transform.position.x;
        if (direction != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Sign(direction) * Mathf.Abs(starterScale.x);
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw spheres in the editor to visualize the attack ranges
        if (attackPointQuick != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPointQuick.position, attackRangeQuick);
        }
        if (attackPointHeavy != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPointHeavy.position, 2f);
        }
    }
}
