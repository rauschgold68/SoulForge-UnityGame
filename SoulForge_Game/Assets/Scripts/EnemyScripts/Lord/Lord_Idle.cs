using UnityEngine;

public class Lord_Idle : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get reference to the player and this enemy's Rigidbody2D
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        var playerComponent = player.GetComponent<IPlayer>();
        if (playerComponent != null && playerComponent.GetCurrentHealth() > 0)
        {
            Rigidbody2D lordsBody = animator.GetComponent<Rigidbody2D>();
            float chaseDistance = 7f; // Distance at which the Lord starts chasing the player

            // Calculate horizontal distance to the player
            float distanceToPlayer = Mathf.Abs(player.position.x - lordsBody.position.x);

            // If the player is within chase distance, trigger the Run state
            if (distanceToPlayer < chaseDistance)
            {
                // Zeige die BossBar, wenn sie noch nicht sichtbar ist
                var bossBar = BossBar.Instance;
                if (bossBar != null && !bossBar.barRoot.activeSelf)
                {
                    bossBar.ShowBar();
                }
                animator.SetTrigger("Run");
            }
            // Otherwise, remain in Idle
        }
    }

    // The following methods are available for further state handling if needed:

    // Called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Cleanup or reset logic can go here
    //}

    // Called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // Called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
