using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySegmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int HealthPoints = 10;
    private float iFrame = 1;
    private float iCurrentPeriod;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iCurrentPeriod -= Time.deltaTime;
    }
    public void Hit(int damage)
    {
        if (iCurrentPeriod <= 0)
        {
            HealthPoints -= damage;
            iCurrentPeriod = iFrame;
            if (HealthPoints <= 0)
            {
                DestroyBodyPart();
            }
        }

    }
    public void DestroyBodyPart()
    {
        GetComponentInParent<MainGameLoop>().RemoveBodyPart(gameObject);
        // do death animation for body part
        Destroy(gameObject);
    }
}
