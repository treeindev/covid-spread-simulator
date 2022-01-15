using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Generator : MonoBehaviour
{
    public ArrayList spheres = new ArrayList();
    public Material sphereMaterial;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<1000; i++)
        {
            // Create cordinates
            float x_position = Random.Range(-4.0f, 4.0f);
            float y_position = 0.3f;
            float z_position = Random.Range(-4.0f, 3.3f);

            // Create object on game
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Set object properties
            sphere.name = "sphere_" + i;
            sphere.transform.position = new Vector3(x_position, y_position, z_position);
            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sphere.AddComponent<Rigidbody>();
            sphere.AddComponent<Scene3Subject>();

            // Add new object to collection
            spheres.Add(spheres);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
