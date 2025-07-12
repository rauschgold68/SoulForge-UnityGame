using UnityEngine;

public class SoulSpawn : MonoBehaviour
{
    [Header("Assign Soul Prefab")]
    public GameObject soulPrefab;
    [Header("Spawn Point (Transform)")]
    public Transform spawnPoint;

    /// <summary>
    /// Spawns a soul prefab at the spawnPoint position, or at this object's position if no spawnPoint is set.
    /// </summary>
    public void SpawnSoul()
    {
        if (soulPrefab == null)
        {
            Debug.LogWarning("SoulSpawn: soulPrefab is not assigned!");
            return;
        }
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(soulPrefab, spawnPos, Quaternion.identity);
    }
}
