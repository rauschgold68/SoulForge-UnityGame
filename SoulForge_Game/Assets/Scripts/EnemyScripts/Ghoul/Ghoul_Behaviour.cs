using UnityEngine;
using SoulForge;

public class Ghoul_Behaviour : MonoBehaviour, IEnemy
{

    public Animator animator; // Reference to the Animator component
    public Transform player; // Reference to the player transform

    // Make these fields public so the StateMachineBehaviour can access them
    public int maxHealth = 50;
    public int currentHealth;

    public bool isImmune = false; // True if Ghoul is currently immune to damage
    public float immunityDuration = 0.7f; // Duration of immunity in seconds
    public float immunityTimer = 0f; // Timer for tracking immunity

    private Rigidbody2D rb;

    public Transform explosionPoint; // Center point for explosion damage
    private float explosionRadius = 1.7f; // Radius of explosion damage
    private int explosionDamage = 30; // Damage dealt by explosion
    public LayerMask playerLayer; // LayerMask to identify player(s)

    public Vector3 starterScale; // Placeholder for the starter scale

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        starterScale = transform.localScale; // Initialize starterScale
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

    void Die()
    {
        Debug.Log("Ghoul has died.");

        animator.SetBool("IsDead", true); // Set the IsDead parameter to true in the Animator

        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
    }

    public void Revive()
    {
        Debug.Log("Ghoul has revived.");
        currentHealth = maxHealth; // Reset health to max
        animator.SetBool("IsDead", false); // Reset the IsDead parameter in the Animator
        GetComponent<Collider2D>().enabled = true; // Re-enable the collider
        // Stop all movement
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
        // Flip to starter side
        if (starterScale != Vector3.zero) transform.localScale = starterScale;
        // Reset aggro/target here if needed
    }

    // Utility method for state machine to check if Ghoul is immune
    public bool IsImmune() { return isImmune; }

    /// <summary>
    /// Triggers an explosion at the explosionPoint, damaging all players in range.
    /// Call this from an Animation Event.
    /// </summary>
    public void Explode()
    {
        if (explosionPoint == null) return;
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(explosionPoint.position, explosionRadius, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            IPlayer playerComponent = player.GetComponent<IPlayer>();
            playerComponent?.TakeDamage(explosionDamage);
        }
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
        // Draw the explosion radius in the editor for visualization
        if (explosionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(explosionPoint.position, explosionRadius);
        }
    }

    public void SetImmune(bool value)
    {
        isImmune = value;
        if (value)
        {
            immunityTimer = immunityDuration; // Reset timer when becoming immune
        }
    }
}
