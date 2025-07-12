using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    private float _speed = 3f;
    public float Speed { get => _speed; set => _speed = value; }

    private Vector2 movement;
    private float horizontalMoveX = 0f;
    private float horizontalMoveY = 0f;
    private bool movementEnabled = true;

    public void SetMovementEnabled(bool enabled)
{
    movementEnabled = enabled;

    if (!enabled)
    {
        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero; // wichtig, um sofort anzuhalten
        animator.SetFloat("moveSpeed", 0f);
    }
}


    void Update()
    {
        if (!movementEnabled) return;

        horizontalMoveX = Input.GetAxisRaw("Horizontal");
        horizontalMoveY = Input.GetAxisRaw("Vertical");

        movement.x = horizontalMoveX;
        movement.y = horizontalMoveY;

        animator.SetFloat("moveSpeed", Mathf.Abs(movement.magnitude));
    }

    void FixedUpdate()
{
    if (movementEnabled)
    {
        rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
    }

    if (movement.x != 0)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(movement.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}

}
