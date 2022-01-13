using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Transform middleSegment;
    public float speed = 0.15f;
    public int dotDamage = 1;
    public bool homing = false;
    public bool piercing = false;
    public bool retargeting = false;
    public int bounces = 0;
    public bool dot = false;
    public float dotTick = 0.1f;
    public float currentDotDelay = 0;
    public float radius;
    private Collider collider;
    private GameObject target;
    private List<GameObject> oldTargets = new List<GameObject>();
    public MeshRenderer gameBoard;
    void Start()
    {
        collider = GetComponent<Collider>();
        target = Utils.FindClosestWithTag(transform, "Enemies");
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(target.transform);
        var currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 loc = transform.position;
        loc.y = gameBoard.bounds.center.y;
        if (!gameBoard.bounds.Contains(loc))
            Destroy(gameObject);
        if(retargeting && target == null)
        {
            target = Utils.FindClosestWithTag(transform, "Enemies");
        }
        transform.position += transform.forward * speed * Time.deltaTime;
        if (homing && target != null)
            transform.LookAt(target.transform);

        if(dot)
        {
            currentDotDelay -= Time.deltaTime;
            if (currentDotDelay < 0)
            {
                currentDotDelay = dotTick;
                Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider enemy in hitEnemies)
                {
                    if (enemy.GetComponent<BaseEnemyScript>())
                    {
                        enemy.GetComponent<BaseEnemyScript>().Hit(dotDamage);
                    }
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Physics.IgnoreCollision(collision.collider, collider);
                break;
            case "Enemies":
                if (!piercing && bounces == 0)
                    Destroy(gameObject);
                else
                {
                    Physics.IgnoreCollision(collision.collider, collider);
                    if(bounces > 0)
                    {
                        oldTargets.Add(target);
                        target = Utils.FindClosestWithTag(transform, "Enemies", oldTargets);
                        if(target)
                            transform.LookAt(target.transform);
                        bounces--;
                    }
                }
                break;
            case "BoardEdge":
                Destroy(gameObject);
                break;
        }
        
        
    }
}
