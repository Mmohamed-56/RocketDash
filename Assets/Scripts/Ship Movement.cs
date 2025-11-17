using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float despawnDistance = 50f;
    
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem thrusterParticles;
    [SerializeField] private bool autoFindParticles = true;
    
    private Vector3 startPosition;
    private float distanceTraveled = 0f;
    
    void Start()
    {
        // Store starting position to track distance
        startPosition = transform.position;
        
        // Auto-find particle system if enabled and not assigned
        if (autoFindParticles && thrusterParticles == null)
        {
            thrusterParticles = GetComponentInChildren<ParticleSystem>();
        }
        
        // Start particle effects if found
        if (thrusterParticles != null && !thrusterParticles.isPlaying)
        {
            thrusterParticles.Play();
        }
    }

    void Update()
    {
        // Move the ship forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        // Calculate distance traveled
        distanceTraveled = Vector3.Distance(startPosition, transform.position);
        
        // Despawn when distance threshold is reached
        if (distanceTraveled >= despawnDistance)
        {
            DespawnShip();
        }
    }
    
    private void DespawnShip()
    {
        // Stop particles before despawning
        if (thrusterParticles != null)
        {
            thrusterParticles.Stop();
        }
        
        // Destroy the ship GameObject
        Destroy(gameObject);
    }
    
    // Optional: Draw gizmo in editor to visualize despawn distance
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, despawnDistance);
    }
}