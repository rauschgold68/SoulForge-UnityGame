using SoulForge;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator; // Reference to the Animator component

    public Transform attackPoint; // Point from where the attack originates

    public LayerMask enemyLayers; // Layer mask to identify enemy layers


    // ---Player Parameters ---
    public float attackRange = 0.5f; // Range of the attack
    public int attackDamage = 10; // Damage dealt by the attack
    public float attackRate = 1.45f; // Attacks per second
    float nextAttackTime = 0f; // Time when the next attack can occur
    // -------------------------

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
        // Check for enemies in the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage all enemies in range
        foreach (Collider2D enemies in hitEnemies)
        {
            IEnemy enemy = enemies.GetComponent<IEnemy>();
            enemy?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to visualize the attack range
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
