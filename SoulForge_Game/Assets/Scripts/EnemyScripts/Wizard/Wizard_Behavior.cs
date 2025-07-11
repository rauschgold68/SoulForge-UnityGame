using UnityEngine;
using SoulForge;

public class Wizard_Behavior : MonoBehaviour, IEnemy
{
    public Animator animator; // Reference to the Animator component

    public int maxHealth = 100;
    public int currentHealth;

    public Vector3 starterScale; // To store the initial scale of the wizard

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        // Store the initial scale of the wizard
        starterScale = transform.localScale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hit"); // Play Hit Animation

        if (currentHealth <= 0)
        {
            Die();
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
