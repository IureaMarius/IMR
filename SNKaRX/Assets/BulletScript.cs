using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Transform middleSegment;
    public float speed = 0.15f;
    public int damage = 1;
    private Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
        player = GameObject.Find("Player");
        middleSegment = player.transform.GetChild(player.transform.childCount / 2);
        transform.LookAt(middleSegment);
        var currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
