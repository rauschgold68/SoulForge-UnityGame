using UnityEngine;
using System.Collections;


public class PlayerHealth : MonoBehaviour, IPlayer
{
    public Animator animator; // Reference to the Animator component
    public HealthBar healthBar;

    // --- Player Health Parameters ---
    private int _maxHealth = 120;
    private int _currentHealth;

    private HandleGameReset gameResetHandler;

    [SerializeField] public GameObject gameOverScreen;

    [SerializeField] public GameObject victoryScreen;

    public int MaxHealth { get => _maxHealth; set { _maxHealth = value; if (healthBar != null) healthBar.setMaxHealth(_maxHealth); } }
    public int CurrentHealth { get => _currentHealth; set { _currentHealth = value; if (healthBar != null) healthBar.setCurrentHealth(_currentHealth); } }

    private void Start()
{
    CurrentHealth = MaxHealth;
    gameResetHandler = FindFirstObjectByType<HandleGameReset>();
    if (healthBar == null) healthBar = FindAnyObjectByType<HealthBar>();
    if (healthBar != null)
    {
        healthBar.setMaxHealth(MaxHealth);
        healthBar.setCurrentHealth(CurrentHealth);
    }
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
    animator.SetBool("IsDead", true); // Death Animation
    GetComponent<Collider2D>().enabled = false;

    if (gameOverScreen != null)
        gameOverScreen.SetActive(true);

    var move = GetComponent<PlayerMovement>();
    if (move != null) move.enabled = false;

    var combat = GetComponent<PlayerCombat>();
    if (combat != null) combat.enabled = false;

    // Entferne den automatischen Reset:
    // StartCoroutine(WaitAndResetGame());
}


private IEnumerator WaitAndResetGame()
{
    yield return new WaitForSeconds(2f); // 2 Sekunden warten

    if (gameResetHandler != null)
        gameResetHandler.CommenceGameReset();
    else
        Debug.LogWarning("GameResetHandler wurde nicht gefunden!");
}


    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
}
