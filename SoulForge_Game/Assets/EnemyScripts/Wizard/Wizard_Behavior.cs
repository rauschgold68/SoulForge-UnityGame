using UnityEngine;
using SoulForge;

public class Wizard_Behavior : MonoBehaviour, IEnemy
{
    public Animator animator; // Reference to the Animator component

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
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
        this.enabled = false; // Disable the script to stop further updates
    }
}
