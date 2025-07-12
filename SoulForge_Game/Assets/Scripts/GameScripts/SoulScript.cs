using UnityEngine;

public class SoulScript : MonoBehaviour
{
    public enum SoulType { GhoulSoul, WizardSoul, GolemSoul, LordsSoul }
    public SoulType soulType;
    private int ghoulSoulValue = 1;
    private int wizardSoulValue = 10;
    private int golemSoulValue = 25;
    // LordSoul does not give a value, but triggers the win condition

    private GameObject player;
    private Rigidbody2D rb;
    private float magnetDistance = 1.2f;
    private float magnetSpeed = 8f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;
        float dist = Vector2.Distance(transform.position, player.transform.position);
        float speed = magnetSpeed;
        switch (soulType)
        {
            case SoulType.GhoulSoul:
                speed = 6f;
                break;
            case SoulType.WizardSoul:
                speed = 5f;
                break;
            case SoulType.GolemSoul:
                speed = 3f;
                break;
            case SoulType.LordsSoul:
                speed = 1f;
                magnetDistance = 0f;
                break;
        }
        if (dist < magnetDistance)
        {
            // Magnet logic: Soul follows the player when in range
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = dir * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void CollectSoul(IPlayer playerComponent)
    {
        var stat = player != null ? player.GetComponent<StatController>() : null;
        switch (soulType)
        {
            case SoulType.GhoulSoul:
                stat?.AddSouls(ghoulSoulValue);
                break;
            case SoulType.WizardSoul:
                stat?.AddSouls(wizardSoulValue);
                break;
            case SoulType.GolemSoul:
                stat?.AddSouls(golemSoulValue);
                break;
            case SoulType.LordsSoul:
                // LordSoul: Trigger win condition
                var lord = FindAnyObjectByType<Lords_Behaviour>();
                if (lord != null)
                {
                    var winMethod = lord.GetType().GetMethod("win", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (winMethod != null) winMethod.Invoke(lord, null);
                }
                break;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles collision with the player and triggers soul collection logic.
    /// </summary>
    /// <param name="other">Collider2D that entered the trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayer playerComponent = other.GetComponent<IPlayer>();
            if (playerComponent != null)
            {
                CollectSoul(playerComponent);
            }
        }
    }
}
