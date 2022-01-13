using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleHealerBodySegmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var level = GetComponent<BodySegmentScript>().level;
        if (level == 2)
        {
            GetComponent<HealerBodySegmentScript>().nrOfTargets *= 2;
        }
        if (level == 3)
        {
            GetComponent<HealerBodySegmentScript>().healAll = true;
            GetComponent<HealerBodySegmentScript>().healPercent *= 2;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
