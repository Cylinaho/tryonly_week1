using UnityEngine;
using UnityEngine.AI;

public class ChasingAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float visionRange = 25f;       // Keep this high so it detects you easily
    public float visionAngle = 180f;      // 180 = full view ahead and to the sides

    [Header("Targets")]
    public Transform player;

    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (CanSeePlayer())
        {
            isChasing = true;
            agent.SetDestination(player.position);
        }
        else
        {
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
        if (distanceToPlayer > visionRange) return false;

        // 2. Check Vision Angle
        Vector3 aiEyePos = transform.position + (Vector3.up * 1.5f); // Fire from higher up (head level)
        Vector3 playerCenterPos = player.position + (Vector3.up * 1f); // Aim for player chest level
        Vector3 directionToPlayer = (playerCenterPos - aiEyePos).normalized;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle) return false;

        // 3. Line of Sight Check (Raycast)
        RaycastHit hit;

        // We throw out a raycast, but we use a loop or filter to make sure it doesn't trip on the floor
        if (Physics.Raycast(aiEyePos, directionToPlayer, out hit, visionRange))
        {
            // If the ray hits our own AI body, ignore it and look past it
            if (hit.transform == this.transform)
            {
                // Re-fire the ray slightly forward to step outside the AI's own body
                if (Physics.Raycast(aiEyePos + directionToPlayer * 0.8f, directionToPlayer, out hit, visionRange))
                {
                    if (hit.transform.CompareTag("Player") || hit.transform == player)
                    {
                        return true;
                    }
                }
            }

            // Normal detection check
            if (hit.transform.CompareTag("Player") || hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}