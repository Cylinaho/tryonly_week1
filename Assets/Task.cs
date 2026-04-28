using UnityEngine;

public class Task : MonoBehaviour
{
    // The variables are outside Update so they don't reset
    float move = 0.05f;
    float rotate = 1.0f;
    
    // We create our own angle tracker
    float currentAngle = 0f;

    void Update()
    {
        // --- Task 1: Movement ---
        transform.position += new Vector3(move, 0, 0);

        // --- Task 2: Movement Boundary ---
        if (transform.position.x > 5f) move = -0.05f;
        if (transform.position.x < -5f) move = 0.05f;

        // --- Task 3: Rotation (Simple Counter) ---
        // 1. Update our own angle variable
        currentAngle += rotate;

        // 2. Apply that angle to the object
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);

        // 3. Rotation Boundary
        if (currentAngle > 45f) rotate = -1.0f;
        if (currentAngle < -45f) rotate = 1.0f;
    }
}