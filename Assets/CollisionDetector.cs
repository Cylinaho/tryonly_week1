using UnityEngine;
using UnityEngine.InputSystem;

// 1. Move the interface OUTSIDE the class so other scripts can easily implement it
public interface IInteractable
{
    int Interact(); 
}

public class CollisionDetector : MonoBehaviour
{
    private int score = 0;
    public int totalItemsToCollect = 1;
    private GameObject currentCollider;

    void OnCollisionEnter(Collision collision)
    {
        currentCollider = collision.gameObject;
    }

    void OnCollisionExit(Collision collision)
    {
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
        if (currentCollider == other.gameObject)
            currentCollider = null;
    }

    void OnInteract(InputValue value)
    {
        if (currentCollider != null)
        {
            print($"Interacting with {currentCollider.name}");

            // 1. Check for Collectibles
            var collectible = currentCollider.GetComponent<Collectible>();
            if (collectible != null)
            {
                score += collectible.scoreValue;
                print($"★ Item Collected! Current Score: {score} / {totalItemsToCollect}");

                if (score >= totalItemsToCollect)
                {
                    print("🏆 You collected all items! You win!");
                }
                
                collectible.Collect();
                currentCollider = null; 
                return; 
            }

            // 2. Check for Doors
            var door = currentCollider.GetComponent<Door>();
            if (door == null) door = currentCollider.GetComponentInParent<Door>();

            if (door != null)
            {
                print($"Interacting with a Door: {currentCollider.name}");
                door.Interact();
                return;
            }

            // 3. Check for Ball physics
            if (currentCollider.CompareTag("Ball"))
            {
                print("Kicking the ball!");
                var ball = currentCollider.GetComponent<Rigidbody>();
                if (ball != null)
                {
                    ball.AddForce(transform.forward * 50f + new Vector3(0, 50f, 0));
                }
                return;
            }

            // 4. Check for Generic IInteractable interface
            var interactable = currentCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                score += interactable.Interact();
                print($"Total Score: {score}");
                
                // Commented out to prevent errors if MyUIManager doesn't exist yet
                // MyUIManager.UpdateScore(score);
                return;
            }
        }
        else
        {
            print("Nothing to interact with at the moment!");
        }
    }

    void Hello(string name)
    {
        print($"Hello, {name}!");
    }
}