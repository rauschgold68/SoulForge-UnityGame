using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using SoulForge;

public class HandleGameReset : MonoBehaviour
{

    [Header("Room Door References")]
    public GameObject door1;

    [Header("Player & Enemy References")]
    public StatController playerStatController;
    public Transform playerTransform;
    public Vector3 playerStartPosition;
    public List<IEnemy> allEnemies; // Assign all enemies in the scene or find at runtime

    public RoomController roomController;

    private Dictionary<IEnemy, Vector3> enemyStartPositions = new Dictionary<IEnemy, Vector3>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optionally auto-assign player references
        if (playerStatController == null) playerStatController = FindFirstObjectByType<StatController>();
        if (playerTransform == null && playerStatController != null) playerTransform = playerStatController.transform;
        // Store player start position if not set
        if (playerStartPosition == Vector3.zero && playerTransform != null) playerStartPosition = playerTransform.position;
        // Auto-find all enemies if not assigned
        if (allEnemies == null || allEnemies.Count == 0)
            allEnemies = new List<IEnemy>(FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IEnemy>());
        // Store enemy start positions
        foreach (var enemy in allEnemies)
            if (enemy != null && !enemyStartPositions.ContainsKey(enemy))
                enemyStartPositions.Add(enemy, ((MonoBehaviour)enemy).transform.position);
    }

    /// <summary>
    /// Call this to fully reset the game: player, stats, and all enemies.
    /// </summary>
    public void CommenceGameReset()
    {
        ResetPlayerPosition();
        ResetPlayerStats();
        RevivePlayer();
        ReviveAllEnemies();
        CullAllFireballs();
        OpenDoor(door1);
        if (roomController != null)
            roomController.ResetRoomStates();
        ResetAllRoomTriggers();
        // Hide Lord BossBar
        var bossBar = FindAnyObjectByType<BossBar>();
        if (bossBar != null)
            bossBar.HideBar();
    }

    public void ResetPlayerPosition()
    {
        if (playerTransform != null)
            playerTransform.position = playerStartPosition;
    }

    public void ResetPlayerStats()
    {
        if (playerStatController != null)
        {
            playerStatController.MaxHealth = 120;
            playerStatController.CurrentHealth = 120;
            playerStatController.BaseDamage = 10;
            playerStatController.AttackRange = 1.5f;
            playerStatController.MoveSpeed = 3f;
            // Add more stat resets as needed
        }
    }

    public void RevivePlayer()
    {
        if (playerStatController != null && playerStatController.playerHealth != null)
        {
            var health = playerStatController.playerHealth;
            health.CurrentHealth = health.MaxHealth;
            health.animator.SetBool("IsDead", false);
            var collider = health.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = true;
            var move = health.GetComponent<PlayerMovement>();
            if (move != null) move.enabled = true;
            var combat = health.GetComponent<PlayerCombat>();
            if (combat != null) combat.enabled = true;
            // Add more revive logic as needed
        }
    }

    public void ReviveAllEnemies()
    {
        foreach (var enemy in allEnemies)
            ReviveEnemy(enemy);
    }

    public void ReviveEnemy(IEnemy enemy)
    {
        if (enemy == null) return;
        var mono = enemy as MonoBehaviour;
        if (mono != null && enemyStartPositions.ContainsKey(enemy))
            mono.transform.position = enemyStartPositions[enemy];
        enemy.Revive(); // Each enemy implements its own Revive logic
    }

    private void CullAllFireballs()
    {
        // Destroy all fireballs in the scene (assumes tag or component is set)
        foreach (var fireball in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<MonoBehaviour>())
        {
            if (fireball.GetType().Name == "Fireball_Script")
            {
                // Stop momentum if needed
                var rb = fireball.GetComponent<Rigidbody2D>();
                if (rb != null) rb.linearVelocity = Vector2.zero;
                Destroy(fireball.gameObject);
            }
        }
    }

    public void OpenDoor(GameObject door)
{
    if (door != null)
        door.SetActive(false); // Set inactive = Tür offen
}


    void Update()
    {
        // For testing: Press 'R' to reset the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            CommenceGameReset();
        }
    }

public void ResetAllRoomTriggers()
{
    var allTriggers = FindObjectsByType<RoomEntranceTrigger>(FindObjectsSortMode.None);
    foreach (var trigger in allTriggers)
    {
        trigger.ResetTrigger();
    }
}

}
