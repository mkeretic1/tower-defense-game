using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    [Header("Spell attributes")]
    public GameObject spellEffect;
    public string spellName;
    public int spellCost;
    public int spellDamage;
    public string spellDescription;

    [Header("Spell info")]
    public GameObject turretInfoUI;
    public float positionOffset;
    public GameObject spellButton;
    public Text spellNameText;
    public Text spellDamageValueText;
    public Text spellRangeValueText;
    public Text fireRateText;
    public Text fireRateValueText;
    public Text abilitiesText;
    public Text abilitiesValueText;
    public Text descriptionText;
    public Text descriptionValueText;
    public Text spellCostText;
    

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;

        spellCostText.text = spellCost.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateSpell();
        }
    }

    public void ActivateSpell()
    {
        if(PlayerStats.money >= spellCost)
        {
            CastSpell();
        }
        else
        {
            GameObject noGoldUI = Instantiate(buildManager.notEnoughMoneyUI);
            Destroy(noGoldUI, 5f);
        }      
    }

    private void CastSpell()
    {
        PlayerStats.money -= spellCost;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(spellDamage);
        }

        GameObject effect = (GameObject)Instantiate(spellEffect);
        Destroy(effect, 5f);
    }

    public void ShowSpellInfo()
    {
        FillSpellInfo();
    }

    private void FillSpellInfo()
    {
        turretInfoUI.SetActive(true);
        turretInfoUI.transform.position = spellButton.transform.position + new Vector3(0f, positionOffset, 0f);

        spellNameText.text = spellName; 
        spellDamageValueText.text = spellDamage.ToString();
        spellRangeValueText.text = "999";
        fireRateText.enabled = false;
        fireRateValueText.enabled = false;
        abilitiesText.enabled = false;
        abilitiesValueText.enabled = false;
        descriptionText.enabled = true;
        descriptionValueText.enabled = true;
        descriptionValueText.text = spellDescription;
    }

    public void HideSpellInfo()
    {
        fireRateText.enabled = true;
        fireRateValueText.enabled = true;
        abilitiesText.enabled = true;
        abilitiesValueText.enabled = true;
        descriptionText.enabled = false;
        descriptionValueText.enabled = false;
        turretInfoUI.SetActive(false);
    }
}
