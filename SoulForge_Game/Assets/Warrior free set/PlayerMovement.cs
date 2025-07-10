using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 3f;

    private Vector2 movement;
    private float horizontalMoveX = 0f;
    private float horizontalMoveY = 0f;
    private bool movementEnabled = true;

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
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
        if (!movementEnabled) return;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(movement.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
