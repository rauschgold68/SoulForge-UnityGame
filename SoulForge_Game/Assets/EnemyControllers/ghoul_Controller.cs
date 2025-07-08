using UnityEngine;
using SoulForge;

public class ghoul_Controller : MonoBehaviour, IEnemy
{

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play Hurt Animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Ghoul has died.");
    }
}
