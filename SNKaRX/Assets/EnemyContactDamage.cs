using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int contactDamage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemies":
                collision.gameObject.GetComponent<BaseEnemyScript>().Hit(contactDamage);
                break;
        }
    }
}
