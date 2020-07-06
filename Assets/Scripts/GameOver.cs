using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text wavesSurvivedNumber;
    public Text enemiesKilledNumber;

    void OnEnable()
    {
        wavesSurvivedNumber.text = PlayerStats.wavesSurvived.ToString();
        enemiesKilledNumber.text = PlayerStats.enemiesKilled.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("GO TO MENU!");
    }
}
