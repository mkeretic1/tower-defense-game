using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [Header("WavesToSpawn")]
    public float waveTimer = 5.5f;
    public Wave[] waves;

    private float countdown = 5f;
    private int waveIndex;

    [HideInInspector]
    public static int enemiesAlive;

    [Header("SetupFields")]
    public Transform spawnPoint;

    public Text waveCounterText;

    public Text enemiesAliveText;

    public GameObject pressToStartWaveUI;

    public GameObject waveStartCountdownUI;
    public Text waveStartCountdownText;

    public GameObject enemyEntranceEffect;

    public static bool levelComplete;

    private bool spawnWave;

    void Start()
    {
        enemiesAlive = 0;
        waveIndex = 0;
        levelComplete = false;
        spawnWave = false;
        waveCounterText.text = waveCounterText.text = "Val " + (waveIndex + 1) + " / " + (waves.Length);
    }

    void Update()
    {      
        enemiesAliveText.text = "Neprijatelja: " + enemiesAlive.ToString();

        if (enemiesAlive > 0)
        {
            waveStartCountdownUI.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnWave = true;
            pressToStartWaveUI.SetActive(false);
            waveStartCountdownUI.SetActive(true);
        }

        if (spawnWave)
        {           
            if (countdown <= 1)
            {
                waveStartCountdownUI.SetActive(false);
                StartCoroutine(SpawnWave());
                countdown = waveTimer;
            }          
        }

        if (waveIndex >= waves.Length)
        {           
            if (PlayerStats.lives > 0)
            {
                levelComplete = true;
            }
            this.enabled = false;
            return;
        }
        else
        {
            if (PlayerStats.lives == 0)
            {
                return;
            }
            waveCounterText.text = "Val " + (waveIndex + 1) + " / " + (waves.Length);

            if (!spawnWave)
            {
                pressToStartWaveUI.SetActive(true);
                return;
            }

            waveStartCountdownText.text = Mathf.Round(countdown).ToString();

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        }             
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        int totalNumOfEnemiesThisWave = getNumberOfEnemiesThisWave(waveIndex, waves);

        enemiesAlive = totalNumOfEnemiesThisWave;

        foreach (WaveGroup waveGroup in wave.waveGroup)
        {         
            for (int i = 0; i < waveGroup.count; i++)
            {              
                SpawnEnemy(waveGroup.enemyPrefab);

                yield return new WaitForSeconds(waveGroup.timeBetweenEnemies);              
            }
        }
        
        waveIndex++;
        PlayerStats.wavesSurvived++;
        spawnWave = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        GameObject effect = (GameObject) Instantiate(enemyEntranceEffect, spawnPoint.position, spawnPoint.rotation);
        Destroy(effect, 5f);
    }


    int getNumberOfEnemiesThisWave(int waveIndex, Wave[] waves)
    {
        Wave wave = waves[waveIndex];

        int totalNumOfEnemiesThisWave = 0;
        foreach (WaveGroup waveGroup in wave.waveGroup)
        {
            totalNumOfEnemiesThisWave += waveGroup.count;
        }
        return totalNumOfEnemiesThisWave;
    }
}
