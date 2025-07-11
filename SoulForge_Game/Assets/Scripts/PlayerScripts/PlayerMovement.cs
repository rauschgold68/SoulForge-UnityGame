using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator animator;

    private float _speed = 3f;
    public float Speed { get => _speed; set => _speed = value; }

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
        rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
        if (movement.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(movement.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
