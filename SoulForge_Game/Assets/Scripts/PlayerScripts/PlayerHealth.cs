using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayer
{
    public Animator animator; // Reference to the Animator component


    // --- Player Health Parameters ---
    private int _maxHealth = 120;
    private int _currentHealth;

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hit"); // Play Hit Animation
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
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
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player has died. Game Over!");
        animator.SetBool("IsDead", true); // Set the IsDead parameter in the Animator to trigger death animation
        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
        var move = GetComponent<PlayerMovement>();
        if (move != null) move.enabled = false;
        var combat = GetComponent<PlayerCombat>();
        if (combat != null) combat.enabled = false;
        // TODO: Add game over logic, such as restarting the level and showing a game over screen
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
}
