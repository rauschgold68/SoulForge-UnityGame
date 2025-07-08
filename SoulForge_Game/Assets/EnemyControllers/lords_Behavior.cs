using UnityEngine;
using SoulForge;

public class lords_Behavior : MonoBehaviour, IEnemy
{

    public Animator animator; // Reference to the Animator component

    public int maxHealth = 2000;
    public int currentHealth;
    public bool lordDefeated = false; // Flag to check if the Lord of Darkness is defeated

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
}
