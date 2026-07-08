using UnityEngine;

public class Coin : MonoBehaviour
{

    public GameObject objectToSpawn;

    public GameObject explosionEffect;
    bool touched = false;

    void Start()
    {
        // This forces the mesh to stand upright (90 degrees on X) when the game begins
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !touched)
        {
            var spawnedObject = Instantiate(objectToSpawn,
            transform.position + new Vector3(0, 1, 0),
            transform.rotation);

            var explosionObject = Instantiate(explosionEffect,
                transform.position + new Vector3(0, 1, 0),
                transform.rotation, spawnedObject.transform);

            touched = true;

            Destroy(gameObject, 1);
        }
    }
}
