using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Welcoming : MonoBehaviour
{
    public Vector3 initialDestinationTarget;
    public Vector3 finalDestinationTarget;
    public float speed = 1.0f;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        // Say hello
        Debug.Log("I am alive: " + transform.name);

        // Assign default material
        var renderer = GetComponent<Renderer>();
        renderer.material.color = name != "sphere_4" ? Color.blue : Color.green;

        // Define their destination paths
        initialDestinationTarget = new Vector3(0.0f, 0.0f, 0.0f);
        finalDestinationTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, initialDestinationTarget, step);
        } else {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, finalDestinationTarget, step);
        }
    }

    IEnumerator OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "sphere_4") {
            var renderer = GetComponent<Renderer>();
            renderer.material.color = Color.red;

            yield return new WaitForSeconds(0.5f);

            canMove = false;
        }
    }
}
