using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayer
{
    public Animator animator; // Reference to the Animator component


    // --- Player Health Parameters ---
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit"); // Play Hit Animation
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int lifeSteal(int damage)
    {
        // TODO: Implement lifeSteal's different states

        int lifeStealAmount = damage / 10; // 10% of damage dealt
        Heal(lifeStealAmount);
        return lifeStealAmount;
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player has died. Game Over!");
        animator.SetBool("IsDead", true); // Set the IsDead parameter in the Animator to trigger death animation

        // Disable player controls or trigger game over logic
        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions

        // TODO: Add game over logic, such as restarting the level and showing a game over screen
    }
}
