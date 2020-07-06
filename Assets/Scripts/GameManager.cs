using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject castleExplodeEffect;
    public GameObject levelCompleteUI;


    public static bool gameOver;

    private void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }

        if (WaveSpawner.levelComplete)
        {
            LevelComplete();
        }
    }

    void EndGame()
    {
        gameOver = true;
        PlayerStats.wavesSurvived--;
        gameOverUI.SetActive(true);
        GameObject effect = (GameObject)Instantiate(castleExplodeEffect, castleExplodeEffect.transform.position, castleExplodeEffect.transform.rotation);
        Destroy(effect, 2f);

        GameObject[] castle = GameObject.FindGameObjectsWithTag("Castle");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in castle)
        {
            Destroy(go, 2f);
        }

        foreach (GameObject go in enemies)
        {
            Destroy(go);
        }


    }

    void LevelComplete()
    {
        gameOver = true;
        levelCompleteUI.SetActive(true);
    }
}
