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

    public CardController cardController; // Assign in Inspector or via code

    // ---Player Parameters ---
    // All fields are now private and not visible/editable in the Inspector
    public float attackRange = 0.5f; // Range of the attack
    public float attackRangeDefault = 0.5f; // Range of the default attack
    public float attackRangeFirstUpgrade = 1f; // Range of the first upgrade attack
    public float attackRangeSecondUpgrade = 1.7f; // Range of the second upgrade attack
    public float attackRangeThirdUpgrade = 2.5f; // Range of the third upgrade attack

    public int attackDamage = 10; // Damage dealt by the attack
    public float attackRate = 1.45f; // Attacks per second
    public float nextAttackTime = 0f; // Time when the next attack can occur

    public bool lifeStealEnabled = false; // Flag to enable/disable life steal
    public string lifeStealUpgradeStage = "Default"; // Current upgrade stage
    // -------------------------

    void Start()
    {
        // Try to auto-assign CardController if not set
        if (cardController == null)
            cardController = FindAnyObjectByType<CardController>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            // Check if the attack button is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Play an attack animation
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate; // Set the next attack time
            }
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = null;
        string upgradeStage = cardController != null ? cardController.GetRangeUpgradeStage() : "Default";
        if (upgradeStage == "Default" && attackPoint != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeDefault, enemyLayers);
        }
        else if (upgradeStage == "FirstUpgrade" && attackPointFirstUpgrade != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointFirstUpgrade.position, attackRangeFirstUpgrade, enemyLayers);
        }
        else if (upgradeStage == "SecondUpgrade" && attackPointSecondUpgrade != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointSecondUpgrade.position, attackRangeSecondUpgrade, enemyLayers);
        }
        else if (upgradeStage == "ThirdUpgrade" && attackPointThirdUpgrade != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPointThirdUpgrade.position, attackRangeThirdUpgrade, enemyLayers);
        }
        else if (attackPoint != null)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        }
        // Damage all enemies in range
        if (hitEnemies != null)
        {
            foreach (Collider2D enemies in hitEnemies)
            {
                IEnemy enemy = enemies.GetComponent<IEnemy>();
                enemy?.TakeDamage(attackDamage);
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                playerHealth?.LifeSteal(attackDamage, lifeStealEnabled, lifeStealUpgradeStage);
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
