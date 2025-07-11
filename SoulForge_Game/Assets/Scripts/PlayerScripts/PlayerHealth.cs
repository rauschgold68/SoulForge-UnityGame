using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayer
{
    public Animator animator; // Reference to the Animator component


    // --- Player Health Parameters ---
    public int maxHealth = 120;
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

    public void LifeSteal(int attackDamage, bool lifeStealEnabled, string upgradeStage)
    {
        if (!lifeStealEnabled) return;
        int lifeStealAmount;
        float lifeStealMultiplier = 0; // Default multiplier for life steal
        switch (upgradeStage)
        {
            case "FirstUpgrade":
                lifeStealMultiplier = 0.1f;
                break;
            case "SecondUpgrade":
                lifeStealMultiplier = 0.2f;
                break;
            case "ThirdUpgrade":
                lifeStealMultiplier = 0.3f;
                break;
        }
        lifeStealAmount = (int)(attackDamage * lifeStealMultiplier);
        Heal(lifeStealAmount);
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

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
