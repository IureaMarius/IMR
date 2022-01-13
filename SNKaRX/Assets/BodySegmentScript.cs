using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodySegmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealthPoints = 10;
    public int HealthPoints = 10;
    public float currentAnimationPeriod;
    public float iFrame = 1;
    public float iCurrentPeriod;
    public GameObject healthBar;
    public Slider healthSlider;
    public int level = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iCurrentPeriod -= Time.deltaTime;
        currentAnimationPeriod -= Time.deltaTime;
        if (currentAnimationPeriod < 0 && healthBar.activeInHierarchy)
            healthBar.SetActive(false);
    }
    public void Hit(int damage)
    {
        if (iCurrentPeriod <= 0)
        {
            HealthPoints -= damage;
            healthSlider.value = (float)HealthPoints / (float)maxHealthPoints;
            healthBar.SetActive(true);
            iCurrentPeriod = iFrame;
            currentAnimationPeriod = iFrame;
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
    public void SetLevel(int x)
    {
        level = x;
    }
}
