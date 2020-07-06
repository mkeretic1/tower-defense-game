using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    public Text moneyText;

    void Update()
    {
        moneyText.text = PlayerStats.money.ToString();
    }
}
