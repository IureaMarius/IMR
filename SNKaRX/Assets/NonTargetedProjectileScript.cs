using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTargetedProjectileScript : BulletScript
{
    private Collider collider;
    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemies":
                if(collider != null)
                    Physics.IgnoreCollision(collision.collider, collider);
                break;
            case "Player":
                Destroy(gameObject);
                break;
            case "BoardEdge":
                Destroy(gameObject);
                break;
        }
        
        
    }
}
