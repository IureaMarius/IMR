using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHealthPoints = 100;
    private int HealthPoints = 100;
    private float iFrame = 1;
    private float iCurrentPeriod;
    public GameObject healthBar;
    public Slider healthSlider;
    private Renderer currentRenderer;
    public Color hitColor = Color.white;
    private Color originalColor;
    public EnemySpawnerScript enemySpawner;

    void Start()
    {
        currentRenderer = GetComponent<Renderer>();
        originalColor = currentRenderer.material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        iCurrentPeriod -= Time.deltaTime;
        if (iCurrentPeriod < 0 && healthBar.activeInHierarchy)
        {
            healthBar.SetActive(false);
            currentRenderer.material.SetColor("_Color", originalColor);
        }
        if (iCurrentPeriod > 0)
        {
            currentRenderer.material.SetColor("_Color", Color.Lerp(originalColor, hitColor, 1f * Mathf.PingPong(Time.time, 1)));
        }
    }
    public void Hit(int damage)
    {
            HealthPoints -= damage;
            healthSlider.value = (float)HealthPoints / (float)maxHealthPoints;
            healthBar.SetActive(true);
            iCurrentPeriod = iFrame;
            if (HealthPoints <= 0)
            {
                Destroy(gameObject);
            }
    }
    private void OnDestroy()
    {
        enemySpawner.EnemyKilled(gameObject);
    }
}
