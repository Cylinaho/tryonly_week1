using UnityEngine;

public class Task : MonoBehaviour
{
    float move = 0.05f;
    float rotate = 1.0f;
    void Start()
    {
        
    }
    void Update()
    {
        // --- Task 1: Movement ---
        transform.position += new Vector3(move, 0, 0);

        // --- Task 2: Move Boundary ---
        if (transform.position.x > 5f) move = -0.05f;
        if (transform.position.x < -5f) move = 0.05f;

        // --- Task 3: Rotation ---
        transform.Rotate(0, rotate, 0);

        // Rotation Boundary
        // Using the raw Y rotation value (decimal between -1 and 1)
        if (transform.rotation.y > 0.3f) rotate = -1.0f;
        if (transform.rotation.y < -0.3f) rotate = 1.0f;
    }
}