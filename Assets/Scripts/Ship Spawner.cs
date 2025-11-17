using UnityEngine;
using System.Collections;

public class ShipSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay = 1f; // Delay before spawning next ship after previous despawns
    
    [Header("Optional Settings")]
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private bool continuousSpawning = true;
    
    private GameObject currentShip;
    private bool isSpawning = false;
    
    void Start()
    {
        // Use this transform as spawn point if none assigned
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
        
        // Spawn first ship if enabled
        if (spawnOnStart)
        {
            SpawnShip();
        }
    }
    
    void Update()
    {
        // Check if we need to spawn a new ship
        if (continuousSpawning && !isSpawning)
        {
            // If no current ship exists, spawn a new one
            if (currentShip == null)
            {
                StartCoroutine(SpawnShipWithDelay());
            }
        }
    }
    
    private IEnumerator SpawnShipWithDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);
        SpawnShip();
        isSpawning = false;
    }
    
    public void SpawnShip()
    {
        if (shipPrefab == null)
        {
            Debug.LogWarning("Ship Prefab is not assigned in Ship Spawner!");
            return;
        }
        
        if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn Point is not assigned in Ship Spawner!");
            return;
        }
        
        // Instantiate the ship at the spawn point
        currentShip = Instantiate(shipPrefab, spawnPoint.position, spawnPoint.rotation);
        
        Debug.Log($"Ship spawned at {spawnPoint.position}");
    }
    
    // Optional: Manually trigger spawn
    public void SpawnShipNow()
    {
        SpawnShip();
    }
    
    // Draw gizmo to visualize spawn point in editor
    void OnDrawGizmos()
    {
        Transform point = spawnPoint != null ? spawnPoint : transform;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(point.position, 0.5f);
        Gizmos.DrawLine(point.position, point.position + point.forward * 2f);
    }
}

