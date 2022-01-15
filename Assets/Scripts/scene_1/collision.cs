using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am inside the Start method.");
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("The Object I have collided with is: " + col.gameObject.GetInstanceID());
    }
}
