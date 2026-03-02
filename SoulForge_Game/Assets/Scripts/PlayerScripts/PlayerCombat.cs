using SoulForge;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    public Transform attackPoint; // Point from where the attack originates
    public Transform attackPointFirstUpgrade; // Point for the first upgrade attack
    public Transform attackPointSecondUpgrade; // Point for the second upgrade attack
    public Transform attackPointThirdUpgrade; // Point for the third upgrade attack

    public LayerMask enemyLayers; // Layer mask to identify enemy layers

    public CardController cardController; // Assign in Inspector

    // ---Player Parameters ---
    private float _attackRange = 0.5f;
    private float _attackRangeDefault = 0.5f;
    private float _attackRangeFirstUpgrade = 1f;
    private float _attackRangeSecondUpgrade = 1.7f;
    private float _attackRangeThirdUpgrade = 2.5f;
    private int _attackDamage = 10;
    private float _attackRate = 1.45f;
    private float _nextAttackTime = 0f;
    private bool _lifeStealEnabled = false;
    private string _lifeStealUpgradeStage = "Default";

    public float AttackRange { get => _attackRange; set => _attackRange = value; }
    public float AttackRangeDefault { get => _attackRangeDefault; set => _attackRangeDefault = value; }
    public float AttackRangeFirstUpgrade { get => _attackRangeFirstUpgrade; set => _attackRangeFirstUpgrade = value; }
    public float AttackRangeSecondUpgrade { get => _attackRangeSecondUpgrade; set => _attackRangeSecondUpgrade = value; }
    public float AttackRangeThirdUpgrade { get => _attackRangeThirdUpgrade; set => _attackRangeThirdUpgrade = value; }
    public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public float AttackRate { get => _attackRate; set => _attackRate = value; }
    public float NextAttackTime { get => _nextAttackTime; set => _nextAttackTime = value; }
    public bool LifeStealEnabled { get => _lifeStealEnabled; set => _lifeStealEnabled = value; }
    public string LifeStealUpgradeStage { get => _lifeStealUpgradeStage; set => _lifeStealUpgradeStage = value; }
    // -------------------------

    void Start()
    {
        // Try to auto-assign CardController if not set
        if (cardController == null)
            cardController = FindAnyObjectByType<CardController>();
    }

    void Update()
    {
        if (Time.time >= NextAttackTime)
        {
            // Check if the attack button is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Play an attack animation
                animator.SetTrigger("Attack");
                NextAttackTime = Time.time + 1f / AttackRate; // Set the next attack time
            }
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = null;
        string upgradeStage = cardController != null ? cardController.GetRangeUpgradeStage() : "Default";
        if (upgradeStage == "Default" && attackPoint != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRangeDefault, enemyLayers);
        }
        else if (upgradeStage == "FirstUpgrade" && attackPointFirstUpgrade != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointFirstUpgrade.position, AttackRangeFirstUpgrade, enemyLayers);
        }
        else if (upgradeStage == "SecondUpgrade" && attackPointSecondUpgrade != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointSecondUpgrade.position, AttackRangeSecondUpgrade, enemyLayers);
        }
        else if (upgradeStage == "ThirdUpgrade" && attackPointThirdUpgrade != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointThirdUpgrade.position, AttackRangeThirdUpgrade, enemyLayers);
        }
        else if (attackPoint != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayers);
        }
        // Damage all enemies in range
        if (hitEnemies != null)
        {
            foreach (Collider2D enemies in hitEnemies)
            {
                IEnemy enemy = enemies.GetComponent<IEnemy>();
                enemy?.TakeDamage(AttackDamage);
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                playerHealth?.LifeSteal(AttackDamage, LifeStealEnabled, LifeStealUpgradeStage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the attack range
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPointFirstUpgrade.position, 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPointSecondUpgrade.position, 1.7f);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackPointThirdUpgrade.position, 2.5f);
        }
    }
}
