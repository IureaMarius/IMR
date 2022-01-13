using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerBodySegmentScript : MonoBehaviour
{
    public float fireDelay;
    private float currentDelay;
    public float healPercent;
    public int nrOfTargets;
    public bool healAll;
    private MainGameLoop game;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLoop>();

        currentDelay = fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay < 0)
        {
            Heal();
            currentDelay = fireDelay;
        }
    }
    private void Heal()
    {
        List<Transform> bodyParts = game.GetBodyParts();

        List<Transform> randomBodyParts = new List<Transform>();
        if(healAll == true)
        {
            randomBodyParts = bodyParts;
        }else
        {
            while(randomBodyParts.Count < nrOfTargets)
            {
                Transform bodypart = bodyParts[Random.Range(0, bodyParts.Count)];
                if(!randomBodyParts.Contains(bodypart))
                {
                    randomBodyParts.Add(bodypart);
                }
            }
        }
        foreach(Transform bodyPart in bodyParts)
        {
            BodySegmentScript bodySegment = bodyPart.gameObject.GetComponent<BodySegmentScript>();
            bodySegment.HealthPoints += (int)((float)bodySegment.maxHealthPoints * healPercent);
            bodySegment.HealthPoints = bodySegment.HealthPoints > bodySegment.maxHealthPoints ? bodySegment.maxHealthPoints : bodySegment.HealthPoints;
            bodySegment.healthSlider.value = (float)bodySegment.HealthPoints / (float)bodySegment.maxHealthPoints;
            if (bodySegment.HealthPoints != bodySegment.maxHealthPoints)
            {
                bodySegment.currentAnimationPeriod = bodySegment.iFrame;
                bodySegment.healthBar.SetActive(true);
            }
        }

    }
}
