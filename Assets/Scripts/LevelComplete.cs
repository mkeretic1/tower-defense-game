using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Text wavesSurvivedNumber;
    public Text enemiesKilledNumber;

    void OnEnable()
    {
        wavesSurvivedNumber.text = PlayerStats.wavesSurvived.ToString();
        enemiesKilledNumber.text = PlayerStats.enemiesKilled.ToString();
    }

    public void NextLevel()
    {
        Debug.Log("GO TO NEXT LEVEL!");
    }

    public void Menu()
    {
        Debug.Log("GO TO MENU!");
    }
}
