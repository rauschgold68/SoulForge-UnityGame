using UnityEngine;
using SoulForge;

public class Wizard_Behavior : MonoBehaviour, IEnemy
{
    public Animator animator; // Reference to the Animator component

    public int maxHealth = 100;
    public int currentHealth;

    public Vector3 starterScale; // To store the initial scale of the wizard

    // Immunity fields
    private bool isImmune = false; // True if Wizard is currently immune to damage
    private float immunityTimer = 0f; // Timer for tracking immunity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        starterScale = transform.localScale;
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
            // Randomize immunity duration (max 0.4s for Wizard)
            float rand = Random.value;
            if (rand < 0.1f)
            {
                // 10% chance: very short immunity (0–0.05s)
                immunityTimer = Random.Range(0f, 0.05f);
            }
            else if (rand < 0.7f)
            {
                // 60% chance: medium immunity (0.1–0.25s)
                immunityTimer = Random.Range(0.1f, 0.25f);
            }
            else
            {
                // 30% chance: max 0.4s
                immunityTimer = 0.4f;
            }
        }
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

    void Die()
    {
        Debug.Log("Wizard has died.");

        animator.SetBool("IsDead", true); // Set the IsDead parameter to true in the Animator

        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
    }

    public void Revive()
    {
        Debug.Log("Wizard has revived.");
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
}
