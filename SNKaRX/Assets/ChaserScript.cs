using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Transform middleSegment;
    public float speed = 0.15f;
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position += new Vector3(0, transform.lossyScale.y / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.childCount > 0)
        {
            middleSegment = player.transform.GetChild(player.transform.childCount / 2);
            transform.LookAt(middleSegment);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
