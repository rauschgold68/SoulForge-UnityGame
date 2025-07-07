using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator; // Reference to the Animator component

    public Transform attackPoint; // Point from where the attack originates
    public float attackRange = 0.5f; // Range of the attack
    public LayerMask enemyLayers; // Layers that represent enemies
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Implement attack logic here
        animator.SetTrigger("Attack");

        // Check for enemies in the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Here you can implement what happens to the enemy when hit
            // For example, you could call a method on the enemy script to apply damage
            Debug.Log("Hit " + enemy.name);
            // enemy.GetComponent<Enemy>().TakeDamage(damageAmount); // Assuming an Enemy script exists with a TakeDamage method
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
