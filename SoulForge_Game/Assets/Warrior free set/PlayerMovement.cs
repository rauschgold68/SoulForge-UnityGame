using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator animator;

    public float speed = 3f;

    Vector2 movement;

    float horizontalMoveX = 0f;
    float horizontalMoveY = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMoveX = Input.GetAxisRaw("Horizontal");
        horizontalMoveY = Input.GetAxisRaw("Vertical");

        movement.x = horizontalMoveX;
        movement.y = horizontalMoveY;

        animator.SetFloat("moveSpeed", Mathf.Abs(movement.magnitude));
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // Flip nur auf der X-Achse
        if (movement.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(movement.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
