using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceIncomingEnemies : MonoBehaviour
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
        if (collision.other.gameObject.tag == "Enemies")
        {
            Vector3 newDirection = Vector3.Reflect(collision.other.transform.forward, collision.contacts[0].normal);
            collision.other.gameObject.GetComponent<Rigidbody>().AddForce(newDirection * 5f);
        }
        
    }
}
