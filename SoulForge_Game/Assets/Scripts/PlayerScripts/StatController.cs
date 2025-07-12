/// <summary>
/// StatController manages and synchronizes all core player stats (health, damage, range, speed) with PlayerHealth, PlayerCombat, and PlayerMovement.
/// 
/// How to use:
/// - Attach to the player GameObject.
/// - Use the public properties (e.g., MaxHealth, BaseDamage) to get/set stats.
/// - Use methods like AddDamage, AddMaxHealth, Heal, etc. to modify stats at runtime.
/// - All stat changes are automatically synchronized with the relevant components.
/// - Example: statController.MaxHealth = 200; statController.AddDamage(10);
/// </summary>
using UnityEngine;

public class StatController : MonoBehaviour
{
    // Core Stats (private fields)
    private int _maxHealth = 120;
    private int _currentHealth = 120;
    private int _baseDamage = 10;
    private float _attackRange = 1.5f;
    private float _moveSpeed = 3f;
    private int _soulAmount = 0;
    private int _soulsThisRound = 0;

    // Public properties for access
    public int MaxHealth { get => _maxHealth; set { _maxHealth = Mathf.Max(1, value); if (playerHealth != null) playerHealth.MaxHealth = _maxHealth; } }
    public int CurrentHealth { get => _currentHealth; set { _currentHealth = Mathf.Clamp(value, 0, MaxHealth); if (playerHealth != null) playerHealth.CurrentHealth = _currentHealth; } }
    public int BaseDamage { get => _baseDamage; set { _baseDamage = Mathf.Max(0, value); if (playerCombat != null) playerCombat.AttackDamage = _baseDamage; } }
    public float AttackRange { get => _attackRange; set { _attackRange = Mathf.Max(0, value); if (playerCombat != null) playerCombat.AttackRange = _attackRange; } }
    public float MoveSpeed { get => _moveSpeed; set { _moveSpeed = Mathf.Max(0, value); if (playerMovement != null) playerMovement.Speed = _moveSpeed; } }
    public int SoulAmount { get => _soulAmount; set { _soulAmount = Mathf.Max(0, value); if (playerHealth != null) playerHealth.SoulAmount = _soulAmount; } }
    public int SoulsThisRound { get => _soulsThisRound; set { _soulsThisRound = Mathf.Max(0, value); if (playerHealth != null) playerHealth.SoulsThisRound = _soulsThisRound; } }
    public void AddSouls(int amount)
    {
        SoulAmount += amount;
        SoulsThisRound += amount;
    }

    public void SetSoulsThisRound(int value)
    {
        SoulsThisRound = value;
    }

    public void SetSoulAmount(int value)
    {
        SoulAmount = value;
    }

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
            playerHealth.MaxHealth = MaxHealth;
            playerHealth.CurrentHealth = CurrentHealth;
        }
        if (playerCombat != null)
        {
            playerCombat.AttackDamage = BaseDamage;
            playerCombat.AttackRange = AttackRange;
        }
        if (playerMovement != null)
        {
            playerMovement.Speed = MoveSpeed;
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
    }

    public void RestoreFullHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void AddDamage(int amount)
    {
        BaseDamage += amount;
    }

    public void AddAttackRange(float amount)
    {
        AttackRange += amount;
    }

    public void AddMoveSpeed(float amount)
    {
        MoveSpeed += amount;
    }

    public void SetMaxHealth(int value)
    {
        MaxHealth = value;
    }
    public void SetBaseDamage(int value)
    {
        BaseDamage = value;
    }
    public void SetAttackRange(float value)
    {
        AttackRange = value;
    }
    public void SetMoveSpeed(float value)
    {
        MoveSpeed = value;
    }

    public void AddMaxHealth(int amount)
    {
        MaxHealth += amount;
    }

    public void AddCurrentHealth(int amount)
    {
        CurrentHealth += amount;
    }

    public void EnableLifeSteal(string upgradeStage)
    {
        if (playerCombat != null)
        {
            playerCombat.LifeStealEnabled = true;
            playerCombat.LifeStealUpgradeStage = upgradeStage;
        }
    }

    public void AddAttackSpeed(float amount)
    {
        if (playerCombat != null)
        {
            playerCombat.AttackRate = Mathf.Max(0.1f, playerCombat.AttackRate + amount);
        }
    }

    public bool IsAlive() => CurrentHealth > 0;
    public float GetHealthPercent() => (float)CurrentHealth / MaxHealth;
}
