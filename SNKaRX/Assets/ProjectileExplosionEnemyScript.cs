using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosionEnemyScript : ChaserScript
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    private void OnDestroy()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 45.0f, 0));
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 90.0f, 0));
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 135.0f, 0));
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 180.0f, 0));
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 225.0f, 0));
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 270.0f, 0));
        Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 315.0f, 0));
    }
}
