using UnityEngine;

public class Fireball_Script : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float lifetime; // Lifetime of the fireball in seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force; // Set speed to the specified force
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= 10f) // Destroy the fireball after 5 seconds
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayer playerComponent = other.GetComponent<IPlayer>();
            if (playerComponent != null)
            {
                playerComponent.TakeDamage(10); // Deal 10 damage to the player
            }
            Destroy(gameObject); // Destroy the fireball on impact
        }
    }
}
