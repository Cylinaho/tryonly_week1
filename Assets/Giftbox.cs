using UnityEngine;

public class Giftbox : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject ballPrefab; // Drag your Ball prefab here in the Inspector
    [SerializeField] private int requiredPresses = 3;

    private int pressCount = 0;
    private bool isPlayerNear = false;

    void Update()
    {
        // If player is inside the zone and presses E
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            pressCount++;
            Debug.Log($"GiftBox pressed {pressCount}/{requiredPresses} times.");

            if (pressCount >= requiredPresses)
            {
                SpawnBallAndDestroy();
            }
        }
    }

    private void SpawnBallAndDestroy()
    {
        if (ballPrefab != null)
        {
            // Spawn the ball where the giftbox currently is
            Instantiate(ballPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Please assign the Ball Prefab in the GiftBox inspector!");
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the object entering has the PlayerCapsule component
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            isPlayerNear = false;
        }
    }
}
