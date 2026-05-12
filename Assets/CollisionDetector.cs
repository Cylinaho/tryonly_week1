using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionDetector : MonoBehaviour
{
    int score = 0;
    public int totalItemsToCollect = 1;
    GameObject currentCollider;

    void OnCollisionEnter(Collision collision)
    {
        currentCollider = collision.gameObject;
    }

    void OnCollisionExit(Collision collision)
    {
        // Only clear if we are leaving the SPECIFIC object we are touching
        if (currentCollider == collision.gameObject)
            currentCollider = null;
    }

    void OnTriggerEnter(Collider other)
    {
        currentCollider = other.gameObject;
        print($"Collided with {currentCollider.name}");
    }

    void OnTriggerExit(Collider other)
    {
        // Only clear if we are leaving the SPECIFIC trigger
        if (currentCollider == other.gameObject)
            currentCollider = null;
    }

    void OnInteract(InputValue value)
    {
        if (currentCollider != null)
        {
            // 1. Check for Collectibles
            var collectible = currentCollider.GetComponent<Collectible>();
            if (collectible != null)
            {
                score += collectible.scoreValue;
                collectible.Collect();
                currentCollider = null; // Clear this so we don't click it twice
                return; // Exit here so we don't check for doors on a dead object
            }

            // 2. Check for Doors (Check the object OR its parent)
            var door = currentCollider.GetComponent<Door>();
            if (door == null) door = currentCollider.GetComponentInParent<Door>();

            if (door != null)
            {
                print($"Interacting with a Door: {currentCollider.name}");
                door.Interact();
            }
        }
    }

    void Hello(string name)
    {
        print($"Hello, {name}!");
    }
}