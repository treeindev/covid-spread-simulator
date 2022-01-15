using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Subject : MonoBehaviour
{
    public bool infected = false;
    private Vector3 currentDestination;
    private bool isGoingToMedic = false;
    private float speed = 1.5f;
    private bool hasMedicalRejection = false;

    // Start is called before the first frame update.
    void Start()
    {
        // Assing default infection state.
        if (name == "sphere_4") {
            infected = true;
            hasMedicalRejection = true;
        }

        // Assign default material.
        var renderer = GetComponent<Renderer>();
        renderer.material.color = name != "sphere_4" ? Color.green : Color.red;

        // Assign default destinations.
        currentDestination = getNewRandomDestination();
    }

    // Update is called once per frame.
    void Update()
    {
        if (!hasMedicalRejection && infected && !isGoingToMedic) {
            currentDestination = getMedicalDestination();
            speed = 2.5f;
            isGoingToMedic = true;
        }

        if (!hasMedicalRejection && infected && nearDestination()) {
            // destroy rigit body
            var rigitProperty = GetComponent<Rigidbody>();
            Destroy(rigitProperty);
            StartCoroutine(healMe());
            return;
        }

        if ((!infected || hasMedicalRejection) && nearDestination()) {
            currentDestination = getNewRandomDestination();
            moveTowardsDestination();
        }

        moveTowardsDestination();
    }

    void OnCollisionEnter(Collision col)
    {
        var instance = col.gameObject.GetComponent<Scene3Subject>();
        if (instance && instance.infected && !infected) {
            infected = true;
            var renderer = GetComponent<Renderer>();
            renderer.material.color = Color.red;
        }
    }

    // Gets a new 3D random destination.
    public Vector3 getNewRandomDestination()
    {
        float x_destination = Random.Range(-4.0f, 4f);
        float y_destination = 0.3f;
        float z_destination = Random.Range(-4.0f, 3f);

        return new Vector3(x_destination, y_destination, z_destination);
    }

    public Vector3 getMedicalDestination()
    {
        float x_destination = Random.Range(-0.4f, 4.7f);
        float y_destination = 0.3f;
        float z_destination = Random.Range(4.2f, 4.7f);

        return new Vector3(x_destination, y_destination, z_destination);
    }

    public void moveTowardsDestination()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, step);
    }

    private bool nearDestination()
    {
        if (Vector3.Distance(transform.position, currentDestination) < 0.1f) {
            return true;
        } else {
            return false;
        }
    }

    private IEnumerator healMe()
    {
        yield return new WaitForSeconds(5.0f);

        var renderer = GetComponent<Renderer>();
        renderer.material.color = Color.green;

        if (this.gameObject.GetComponent<Rigidbody>() == null) {
            this.gameObject.AddComponent<Rigidbody>();
        }
        
        infected = false;
        isGoingToMedic = false;
        speed = 1.5f;
    }
}
