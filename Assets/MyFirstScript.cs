using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("Hello World");
        print(transform.rotation);

        var x = new Vector3(1, 2, 3);
        var y = new Vector3(4, 5, 6);

        print(x + y);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate by 0.1f every frame on the Y axis of the world
        transform.Rotate(0f, 0.1f, 0f, Space.World); 

        // Move up by 0.01f every frame
        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y + 0.01f, 
            transform.position.z
        );

        // Alternative to moving upwards by 0.01f
        var pos = transform.position;
        pos.y += 0.01f;
        transform.position = pos;
        
        // Make the object bigger by 0.1% every frame
        transform.localScale = transform.localScale * 1.001f;

        // Alternative #2 to moving upwards by 0.01f by ChatGPT
        transform.position = new Vector3(0f, 0.01f, 0f);
        // transform.position = transform.position + new Vector3(0f, 0.01f, 0f);
        
        // How to declare a new variable
        var x = 1; // This is an Integer
        var v2 = new Vector2(0, 0);
        var v3 = new Vector3(0, 0, 0);
        var v4 = new Vector4(0, 0, 0, 0); 
    }
}
