using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int scoreValue = 1;

    public void Collect()
    {
        var audio = GetComponent<AudioSource>();
        audio.Play(); // Play the collection sound effect

        // Destory game object after the sound effect has finished playing
        var renderer = GetComponent<Renderer>();
        renderer.enabled = false;

        Destroy(gameObject, 1); // Remove the collectible from the scene
    }
}
