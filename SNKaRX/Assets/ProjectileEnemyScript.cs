using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    private GameObject player;
    private Transform middleSegment;
    public float fireDelay = 0.15f;
    private float currentDelay = 0f;
    public GameObject bullet;
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position += new Vector3(0, transform.lossyScale.y / 2, 0);
    }

    void Update()
    {
        if (player.transform.childCount > 0)
        {
            middleSegment = player.transform.GetChild(player.transform.childCount / 2);
            transform.LookAt(middleSegment);
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                Shoot();
                currentDelay = fireDelay;
            }
        }
        
    }
    private void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
