using UnityEngine;

public class Ghoul_IdleRun : StateMachineBehaviour
{
    // Movement and contact damage logic for Ghoul
    private Rigidbody2D rb;
    private Ghoul_Behaviour ghoul;
    private Transform player;
    private float nextAttackTime;
    private float attackCooldown = 0.5f; // Cooldown between attacks in seconds
    public int attackDamage = 10; // Damage dealt to the player on contact
    private int speed = 2; // Speed at which the Ghoul moves towards the player
    private float chaseDistance = 7f; // Distance at which the Ghoul starts chasing the player


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = animator.GetComponent<Rigidbody2D>();
        ghoul = animator.GetComponent<Ghoul_Behaviour>();
        nextAttackTime = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ghoul == null || player == null || rb == null) return;

        // --- Update immunity timer (fix: immunity must tick down even in StateMachineBehaviour) ---
        if (ghoul.IsImmune())
        {
            ghoul.immunityTimer -= Time.deltaTime;
            if (ghoul.immunityTimer <= 0f)
            {
                ghoul.SetImmune(false);
            }
        }

        // --- Movement towards player if within chase distance ---
        float distanceToPlayer = Mathf.Abs(player.position.x - rb.position.x);
        if (distanceToPlayer < chaseDistance)
        {
            // Move towards player
            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            ghoul.LookAtPlayer();
        }

        // --- Contact damage logic ---
        Collider2D[] hits = Physics2D.OverlapCircleAll(rb.position, 0.3f, ghoul.playerLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player") && Time.time >= nextAttackTime)
            {
                IPlayer playerComponent = hit.GetComponent<IPlayer>();
                if (playerComponent != null)
                {
                    playerComponent.TakeDamage(attackDamage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
