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
            // Start immunity coroutine with random short duration (max 0.4s)
            StartCoroutine(ImmunityCoroutine());
        }
    }

    // No need for Update() for immunity anymore

    private System.Collections.IEnumerator ImmunityCoroutine()
    {
        isImmune = true;
        // Wizard: shorter immunity than Golem (0.05s to 0.4s)
        float rand = Random.value;
        float duration;
        if (rand < 0.1f)
        {
            // 10% chance: very short immunity (0.02–1.3s)
            duration = Random.Range(0.8f, 1.3f);
        }
        else if (rand < 0.7f)
        {
            // 60% chance: medium immunity (0.08–0.8s)
            duration = Random.Range(0.08f, 0.8f);
        }
        else
        {
            // 30% chance: max 0.4s
            duration = 0.4f;
        }
        yield return new WaitForSeconds(duration);
        isImmune = false;
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
