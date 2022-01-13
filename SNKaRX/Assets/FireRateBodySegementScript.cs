using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBodySegementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var level = GetComponent<BodySegmentScript>().level;
        if (level == 2)
        {
            GetComponent<ShooterBodySegment>().fireDelay /= 2;
        }
        if (level == 3)
        {
            GetComponent<ShooterBodySegment>().fireDelay /= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
