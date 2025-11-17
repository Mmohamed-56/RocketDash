using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float amplitude = 1f; // How far up and down the object moves
    [SerializeField] bool invertDirection = false; // If true, moves opposite direction (for alternating pattern)

    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        // Oscillate up and down using sine wave
        // Add π (Mathf.PI) phase offset if invertDirection is true to move opposite direction
        float phaseOffset = invertDirection ? Mathf.PI : 0f;
        float newY = startingPosition.y + Mathf.Sin(Time.time * speed + phaseOffset) * amplitude;
        transform.position = new Vector3(startingPosition.x, newY, startingPosition.z);
    }
}
