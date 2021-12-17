using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceIncomingObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Vector3 newDirection = Vector3.Reflect(collision.other.transform.forward, collision.contacts[0].normal);
        collision.other.transform.rotation = Quaternion.LookRotation(newDirection);
        
    }
}
