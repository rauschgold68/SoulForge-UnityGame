using UnityEngine;

public class Lords_Run : StateMachineBehaviour
{
    // Speed at which the Lord moves towards the player
    private float speed = 2f;

    // Cooldown time between attacks (set dynamically)
    private float attackCooldown;

    // Time when the next attack can occur
    private float nextAttackTime = 0f;

    // Offset to maintain distance from the player during attack
    private float attackOffset = 3.5f;
    
    // Random attack selection
    private int randomAttack;

    Transform player;
    Rigidbody2D lordsBody;
    Lords_Behaviour lords_Behavior;
    IPlayer playerComponent;

    // Called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerComponent = player.GetComponent<IPlayer>();
        lordsBody = animator.GetComponent<Rigidbody2D>();
        lords_Behavior = animator.GetComponent<Lords_Behaviour>();
        lords_Behavior = animator.GetComponent<Lords_Behaviour>();
    }

    // Called on each Update frame while in the Run state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerComponent != null && playerComponent.GetCurrentHealth() <= 0)
        {
            animator.SetTrigger("Idle"); // Transition to Idle state if player is dead
        }
        // Randomly select an attack type for variety
        int v = Random.Range(0, 4); // 0, 1, 2, or 3 (inclusive lower, exclusive upper)
        randomAttack = v; 

        // Make the Lord face the player
        lords_Behavior.LookAtPlayer();


        // Calculate direction and distance to the player
        float direction = Mathf.Sign(player.position.x - lordsBody.position.x);
        float distanceToPlayer = Mathf.Abs(player.position.x - lordsBody.position.x);


        // Calculate the target position, maintaining a vertical offset if needed
        Vector2 target = new Vector2(player.position.x - direction * attackOffset, player.position.y + 1.5f);

        // Move towards the player if the distance is greater than attackOffset
        if (distanceToPlayer > attackOffset)
        {
            Vector2 newPos = Vector2.MoveTowards(lordsBody.position, target, speed * Time.fixedDeltaTime);
            lordsBody.MovePosition(newPos);
        }
        // else: Idle/Stand, no movement

        // Attack only if the Lord is in position (distance is appropriate)
        if (distanceToPlayer <= attackOffset)
        {
            if (Time.time >= nextAttackTime)
            {
                // 0, 1, 2 = quickAttack (75%), 3 = heavyAttack (25%)
                if (randomAttack == 3)
                {
                    attackCooldown = 0.8f; // Set cooldown for heavy attack
                    animator.SetTrigger("heavyAttack"); // Trigger the heavy attack animation
                    nextAttackTime = Time.time + 1f / attackCooldown; // Set the next attack time
                }
                else
                {
                    attackCooldown = 3.0f; // Set cooldown for normal attack
                    animator.SetTrigger("quickAttack"); // Trigger the quick attack animation
                    nextAttackTime = Time.time + 1f / attackCooldown; // Set the next attack time
                }
            }
        }
    }

    // Called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Cleanup or reset logic can go here if needed
    }
}