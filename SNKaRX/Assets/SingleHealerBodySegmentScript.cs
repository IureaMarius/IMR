using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHealerBodySegmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var level = GetComponent<BodySegmentScript>().level;
        if (level == 2)
        {
            GetComponent<HealerBodySegmentScript>().healPercent *= 2;
        }
        if (level == 3)
        {
            GetComponent<HealerBodySegmentScript>().fireDelay /= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
