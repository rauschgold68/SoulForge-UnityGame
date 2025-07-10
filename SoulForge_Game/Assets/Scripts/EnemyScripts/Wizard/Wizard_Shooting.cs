using UnityEngine;

public class Wizard_Shooting : MonoBehaviour
{

    public GameObject fireball;
    public Transform fireSpawnPoint;
    public Animator animator;
    
    public void ShootFireball()
    {
        if (fireball != null && fireSpawnPoint != null)
        {
            Instantiate(fireball, fireSpawnPoint.position, fireSpawnPoint.rotation);
            animator.SetBool("IsShooting", false); // Reset the shooting state
        }
        else
        {
            Debug.LogWarning("Fireball or FireSpawnPoint is not set in the Wizard_Shooting script.");
        }
    }
}
