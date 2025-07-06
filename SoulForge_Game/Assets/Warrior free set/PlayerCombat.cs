using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator; // Reference to the Animator component
    // Update is called once per frame
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
    }
}
