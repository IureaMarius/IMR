using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CactusAnimationToggle : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Animator otherAnimator;
    public Transform otherObject;
    public Text text;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        otherAnimator = otherObject.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, otherObject.position);
        animator.SetFloat("distance", distance);
        otherAnimator.SetFloat("distance", distance);
        text.text = distance.ToString();
        
    }
}
