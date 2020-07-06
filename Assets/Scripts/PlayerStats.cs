using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startMoney = 400;

    public static int lives;
    public int startLives = 20;

    public static int wavesSurvived;
    public static int enemiesKilled;

    void Start()
    {      
        money = startMoney;
        lives = startLives;

        wavesSurvived = 0;
        enemiesKilled = 0;
    }
}
