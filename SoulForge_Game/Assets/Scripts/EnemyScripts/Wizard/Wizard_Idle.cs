using UnityEngine;

public class Wizard_Idle : StateMachineBehaviour
{
    // Distance at which the wizard starts shooting at the player
    public float shootDistance = 7f;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;
        float distanceToPlayer = Mathf.Abs(player.position.x - animator.transform.position.x);
        if (player.GetComponent<IPlayer>().GetCurrentHealth() > 0)
        {
            if (distanceToPlayer < shootDistance)
            {
                animator.SetBool("IsShooting", true); // Trigger shooting if within distance
            }
        }
    }
}
