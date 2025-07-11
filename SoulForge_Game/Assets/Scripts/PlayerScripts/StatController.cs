using UnityEngine;

public class StatController : MonoBehaviour
{
    // Core Stats
    public int maxHealth = 120;
    public int currentHealth = 120;
    public int baseDamage = 10;
    public float attackRange = 1.5f;
    public float moveSpeed = 3f;

    public PlayerHealth playerHealth;
    public PlayerCombat playerCombat;
    public PlayerMovement playerMovement;

    void Awake()
    {
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
        if (playerCombat == null) playerCombat = GetComponent<PlayerCombat>();
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.maxHealth = maxHealth;
            playerHealth.currentHealth = currentHealth;
        }
        if (playerCombat != null)
        {
            playerCombat.attackDamage = baseDamage;
            playerCombat.attackRange = attackRange;
        }
        if (playerMovement != null)
        {
            playerMovement.speed = moveSpeed;
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (playerHealth != null)
        {
            playerHealth.currentHealth = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (playerHealth != null)
        {
            playerHealth.currentHealth = currentHealth;
        }
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        if (playerHealth != null)
        {
            playerHealth.currentHealth = currentHealth;
        }
    }

    public void AddDamage(int amount)
    {
        baseDamage = Mathf.Max(0, baseDamage + amount);
        if (playerCombat != null) playerCombat.attackDamage = baseDamage;
    }

    public void AddAttackRange(float amount)
    {
        attackRange = Mathf.Max(0, attackRange + amount);
        if (playerCombat != null) playerCombat.attackRange = attackRange;
    }

    public void AddMoveSpeed(float amount)
    {
        moveSpeed = Mathf.Max(0, moveSpeed + amount);
        if (playerMovement != null) playerMovement.speed = moveSpeed;
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = Mathf.Max(1, value);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (playerHealth != null)
        {
            playerHealth.maxHealth = maxHealth;
            playerHealth.currentHealth = currentHealth;
        }
    }
    public void SetBaseDamage(int value)
    {
        baseDamage = Mathf.Max(0, value);
        if (playerCombat != null) playerCombat.attackDamage = baseDamage;
    }
    public void SetAttackRange(float value)
    {
        attackRange = Mathf.Max(0, value);
        if (playerCombat != null) playerCombat.attackRange = attackRange;
    }
    public void SetMoveSpeed(float value)
    {
        moveSpeed = Mathf.Max(0, value);
        if (playerMovement != null) playerMovement.speed = moveSpeed;
    }

    public void AddMaxHealth(int amount)
    {
        maxHealth = Mathf.Max(1, maxHealth + amount);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (playerHealth != null)
        {
            playerHealth.maxHealth = maxHealth;
            playerHealth.currentHealth = currentHealth;
        }
    }

    public void AddCurrentHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (playerHealth != null)
        {
            playerHealth.currentHealth = currentHealth;
        }
    }

    public void EnableLifeSteal(string upgradeStage)
    {
        if (playerCombat != null)
        {
            playerCombat.lifeStealEnabled = true;
            playerCombat.lifeStealUpgradeStage = upgradeStage;
        }
    }

    public void AddAttackSpeed(float amount)
    {
        if (playerCombat != null)
        {
            playerCombat.attackRate = Mathf.Max(0.1f, playerCombat.attackRate + amount);
        }
    }

    public bool IsAlive() => currentHealth > 0;
    public float GetHealthPercent() => (float)currentHealth / maxHealth;
}
