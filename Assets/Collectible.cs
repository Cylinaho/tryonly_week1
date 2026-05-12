using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int scoreValue = 1;

    public void Collect()
    {
        Destroy(gameObject); // Remove the collectible from the scene
    }
}
