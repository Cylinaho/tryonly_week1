using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float kickForce = 15f; 
    [SerializeField] private float upwardLift = 8f; // Bumped up slightly for a better arc 

    private Rigidbody rb;
    private Transform playerObject; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerObject != null && Input.GetKeyDown(KeyCode.E))
        {
            Kick();
        }
    }

    private void Kick()
    {
        // 1. Completely clear velocities so old physics don't fight the kick
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 2. Slightly lift the ball off the ground to avoid immediate friction/collision issues
        // This breaks the friction/collision clamp holding it down!
        transform.position += new Vector3(0f, 0.1f, 0f);

        // 3. Get horizontal direction away from player
        Vector3 kickDirection = transform.position - playerObject.position;
        kickDirection.y = 0; 
        kickDirection = kickDirection.normalized;

        // 4. Combine horizontal direction and upward lift into ONE diagonal force vector
        Vector3 finalForce = (kickDirection * kickForce) + (Vector3.up * upwardLift);

        // 5. Fire the ball into the air instantly
        rb.AddForce(finalForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            playerObject = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            playerObject = null;
        }
    }
}