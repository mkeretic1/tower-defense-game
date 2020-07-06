using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int value = 20;

    private bool moneyGained = false;


    [Header("Unity stuff")]
    public GameObject deathEffect;

    public Image healthBar;
    public Text valueText;

    private float slowTimer;
    private EnemyBodyColor bodyColor;

    //Gain/lose Gold UI
    BuildManager buildManager;
    private Quaternion gainGoldUIRotation;

    void Start()
    {
        health = startHealth;
        speed = startSpeed;
        valueText.text = this.value.ToString();
        bodyColor = this.GetComponentInChildren<EnemyBodyColor>();
        slowTimer = 0;
        buildManager = BuildManager.instance;
        gainGoldUIRotation = transform.rotation * Quaternion.Euler(0,180,0);
    }

    public void TakeDamage (float damageAmount)
    {
        health -= damageAmount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {         
            Die();
        }
    }

    public void SlowDownEnemy (float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
        bodyColor.ChangeBodyColor();
        slowTimer = 0.5f;
    }

    public void ReturnStartSpeed()
    {
        if (slowTimer <= 0)
        {
            speed = startSpeed;
            bodyColor.ReturnStartColor();
        }      
    }

    void Update()
    {
        slowTimer -= Time.deltaTime;
        ReturnStartSpeed();
    }

    void Die()
    {
        if (moneyGained)
        {
            return;
        }

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        //spawn gold fadeaway after enemy is killed
        buildManager.gainLoseGoldUI.goldFadeawayCanvas(transform.position, gainGoldUIRotation , ("+" + this.value.ToString()));

        Destroy(gameObject);
        moneyGained = true;
        PlayerStats.money += value;
        PlayerStats.enemiesKilled++;
        WaveSpawner.enemiesAlive--;
    }   
}
