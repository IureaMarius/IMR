using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBodySegment : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float fireDelay;
    public int burstSize = 1;
    private float currentFireDelay;
    public float burstDelay = 0;
    private float currentBurstDelay = 0;
    private int currentBurstProjectile = 0;
    public float projectileSpawnOffset = 0;
    public bool homing;
    public int bounces;
    public bool dot;
    public int dotDamage;
    public float dotTick;
    public bool piercing;
    public float radius;
    public bool retargeting;
    public float speed;
    public int damage;
    private MeshRenderer gameBoardMesh;

    public MainGameLoop game;
    // Start is called before the first frame update
    void Start()
    {
        currentFireDelay = fireDelay;
        gameBoardMesh = GameObject.FindGameObjectWithTag("GameController").GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFireDelay < 0)
        {
            currentFireDelay = fireDelay;
            currentBurstProjectile = burstSize;

        }
        if(currentBurstProjectile > 0 && currentBurstDelay < 0)
        {
            currentBurstDelay = burstDelay;
            currentBurstProjectile--;
            Vector3 loc = transform.position + transform.forward * currentBurstProjectile * projectileSpawnOffset;
            loc.y = gameBoardMesh.bounds.center.y;
            if (gameBoardMesh.bounds.Contains(loc))
            {
                var bullet = Instantiate(bulletPrefab, transform.position + transform.forward * currentBurstProjectile * projectileSpawnOffset, transform.rotation);
                var bulletScript = bullet.GetComponent<PlayerBulletScript>();
                bulletScript.gameBoard = gameBoardMesh;
                bulletScript.homing = homing;
                bulletScript.bounces = bounces;
                bulletScript.dot = dot;
                bulletScript.dotDamage = dotDamage;
                bulletScript.dotTick = dotTick;
                bulletScript.piercing = piercing;
                bulletScript.radius = radius;
                bulletScript.retargeting = retargeting;
                bulletScript.speed = speed;
                bullet.GetComponent<EnemyContactDamage>().contactDamage = damage;
            }
            
        }
        currentFireDelay -= Time.deltaTime;
        currentBurstDelay -= Time.deltaTime;
        
    }
}
