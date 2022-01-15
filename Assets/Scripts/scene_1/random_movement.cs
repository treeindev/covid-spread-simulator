using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class random_movement : MonoBehaviour
{
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(
            transform.position, new Vector3(0.0f, 0.0f, 0.0f), step
        );
    }
}
