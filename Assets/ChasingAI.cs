using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent

public class ChasingAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float visionRange = 10f;       // How far the AI can see
    public float visionAngle = 45f;       // The cone of vision (angle)

    [Header("Targets")]
    public Transform player;              // Reference to the Player's transform

    private NavMeshAgent agent;
    private Vector3 originalPosition;     // Stores the starting position
    private bool isChasing = false;

    void Start()
    {
        // Get the NavMeshAgent component attached to this GameObject
        agent = GetComponent<NavMeshAgent>();

        // Save the starting position so we can return to it later
        originalPosition = transform.position;

        // Automatically find the player by tag if not assigned manually
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Check if the player is within sight
        if (CanSeePlayer())
        {
            isChasing = true;
            // Target the player's position
            agent.SetDestination(player.position);
        }
        else
        {
            // If we were chasing but lost the player, head back home
            if (isChasing)
            {
                isChasing = false;
                agent.SetDestination(originalPosition);
            }
        }
    }

    bool CanSeePlayer()
    {
        // 1. Check Distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > visionRange)
        {
            return false; // Player is too far away
        }

        // Adjust positions up by 1 unit to simulate eye-level/center level
        Vector3 aiEyePos = transform.position + Vector3.up;
        Vector3 playerCenterPos = player.position + Vector3.up;

        // 2. Check Vision Angle (Field of View Cone) using the adjusted positions
        Vector3 directionToPlayer = (playerCenterPos - aiEyePos).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle)
        {
            return false; // Player is outside the AI's viewing angle
        }

        // 3. Line of Sight Check (Raycast)
        RaycastHit hit;
        // We shoot from AI eye level directly to Player center level
        if (Physics.Raycast(aiEyePos, directionToPlayer, out hit, visionRange))
        {
            // Debug line so you can visually see the raycast in the Scene tab while playing
            Debug.DrawLine(aiEyePos, hit.point, Color.red);

            if (hit.transform.CompareTag("Player") || hit.transform == player)
            {
                return true; // The ray successfully hit the player!
            }
        }

        return false; // Something else blocked the raycast
    }

    // Visualizes the vision range in the Unity Editor Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}

