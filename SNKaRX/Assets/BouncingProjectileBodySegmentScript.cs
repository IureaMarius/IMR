using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectileBodySegmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var level = GetComponent<BodySegmentScript>().level;
        if (level == 2)
        {
            GetComponent<ShooterBodySegment>().fireDelay /= 1.5f;
            GetComponent<ShooterBodySegment>().bounces += 2;
        }
        if (level == 3)
        {
            GetComponent<ShooterBodySegment>().fireDelay /= 3;
            GetComponent<ShooterBodySegment>().bounces += 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
