﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Text wavesSurvivedNumber;
    public Text enemiesKilledNumber;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    void OnEnable()
    {
        wavesSurvivedNumber.text = PlayerStats.wavesSurvived.ToString();
        enemiesKilledNumber.text = PlayerStats.enemiesKilled.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName); ;
    }
}
