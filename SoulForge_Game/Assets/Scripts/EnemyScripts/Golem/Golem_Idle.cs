using UnityEngine;

public class Golem_Idle : StateMachineBehaviour
{
    // Called on each Update frame while in the Idle state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        var golem = animator.GetComponent<Golem_Behaviour>();
        var playerComponent = player.GetComponent<IPlayer>();
        if (playerComponent != null && playerComponent.GetCurrentHealth() > 0)
        {
            Rigidbody2D golemBody = animator.GetComponent<Rigidbody2D>();
            float chaseDistance = 10f; // Distance at which the Golem starts chasing the player
            float distanceToPlayer = Mathf.Abs(player.position.x - golemBody.position.x);
            if (distanceToPlayer < chaseDistance)
            {
                golem.isChasing = true;
                animator.SetTrigger("Run");
            }
            // Otherwise, remain in Idle
        }
    }
}
