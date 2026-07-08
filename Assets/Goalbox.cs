using UnityEngine;

public class Goalbox : MonoBehaviour
{
private static int currentScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Ignore trigger colliders to prevent false positives
        if (other.isTrigger) return;
        
        // Checks if the object entering the goal zone has the Ball component script
        if (other.GetComponent<Ball>() != null)
        {
            currentScore++;
            Debug.Log($"GOAL! Current Score: {currentScore}");
        }
    }
}