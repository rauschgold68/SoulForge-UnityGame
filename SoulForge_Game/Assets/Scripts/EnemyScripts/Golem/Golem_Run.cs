using UnityEngine;

public class Golem_Run : StateMachineBehaviour
{
    // Speed at which the Golem moves towards the player
    private float speed = 3.5f;
    // Offset to maintain distance from the player during attack
    private float attackOffset = 2f;
    // Cooldown time between attacks (set dynamically)
    private float attackCooldown;
    // Time when the next attack can occur
    private float nextAttackTime = 0f;
    // Random action selection

    // Probability to heal per frame while running (e.g., 2%)
    private float healChancePerFrame = 0.08f;
    private bool isHealing = false;

    Transform player;
    Rigidbody2D golemBody;
    Golem_Behaviour golemBehaviour;
    IPlayer playerComponent;

    // Called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerComponent = player.GetComponent<IPlayer>();
        golemBody = animator.GetComponent<Rigidbody2D>();
        golemBehaviour = animator.GetComponent<Golem_Behaviour>();
    }

    // Called on each Update frame while in the Run state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerComponent != null && playerComponent.GetCurrentHealth() <= 0)
        {
            animator.SetTrigger("Idle");
            return;
        }
        var golem = animator.GetComponent<Golem_Behaviour>();
        if (!golem.isChasing)
        {
            animator.SetTrigger("Idle");
            return;
        }
        // Make the Golem face the player
        golem.LookAtPlayer(player);

        // Calculate direction and distance to the player
        float direction = player.position.x - golemBody.position.x;
        float absDirection = Mathf.Sign(direction);
        float distanceToPlayer = Mathf.Abs(player.position.x - golemBody.position.x);

        // --- Healing logic while running ---
        // Only heal if not already healing or immune
        if (!golemBehaviour.IsImmune() && !isHealing)
        {
            if (Random.value < healChancePerFrame)
            {
                isHealing = true;
                animator.SetTrigger("Heal");
                // Healing and immunity will be handled in Golem_Behaviour
                return; // Skip movement/attack this frame
            }
        }

        // Calculate the target position
        Vector2 target = new Vector2(player.position.x - absDirection * attackOffset, player.position.y + 0.5f);

        // Move towards the player if the distance is greater than attackOffset
        if (distanceToPlayer > attackOffset)
        {
            Vector2 newPos = Vector2.MoveTowards(golemBody.position, target, speed * Time.fixedDeltaTime);
            golemBody.MovePosition(newPos);
        }
        // else: Idle/Stand, no movement

        // --- Attack logic (no healing here anymore) ---
        if (distanceToPlayer <= attackOffset)
        {
            if (Time.time >= nextAttackTime)
            {
                // Only choose between Quick and Heavy
                int v = Random.Range(0, 4); // 0-3
                if (v == 3)
                {
                    attackCooldown = 1.0f;
                    animator.SetTrigger("Heavy"); // Trigger the heavy attack animation
                    nextAttackTime = Time.time + 1f / attackCooldown;
                }
                else
                {
                    attackCooldown = 2.0f;
                    animator.SetTrigger("Quick"); // Trigger the quick attack animation
                    nextAttackTime = Time.time + 1f / attackCooldown;
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

// Utility method for Golem_Behaviour to check immunity
// (Add this to Golem_Behaviour.cs)
// public bool IsImmune() { return isImmune; }
