using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Flip nur auf der X-Achse
        if (movement.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(movement.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
