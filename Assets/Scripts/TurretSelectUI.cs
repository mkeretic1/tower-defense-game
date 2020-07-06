using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectUI : MonoBehaviour
{
    [Header("Upgrade / Sell")]
    public GameObject ui;
    private TurretNode target;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellCost;

    [Header("Turret info")]
    public Text turretNameText;
    public Text damageValueText;
    public Text rangeValueText;
    public Text fireRateValueText;
    public Text abilitiesValueText;

    [Header("Upgrade info")]
    public Text damageValueUpgradeText;
    public Text rangeValueUpgradeText;
    public Text fireRateValueUpgradeText;

    //show turret range
    private Turret turret;

    public void SetTarget( TurretNode target)
    {
        this.target = target;

        ShowTurretRange(false);

        turret = target.turret.GetComponent<Turret>();

        transform.position = target.transform.position + target.positionOffset;

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
            sellCost.text = target.turretBlueprint.GetSellAmount().ToString();
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            sellCost.text = target.turretBlueprint.GetUpgradedSellAmount().ToString();
            HideUpgradeInfo();
        }

        

        ui.SetActive(true);
        ShowTurretRange(true);

        FillUiInfo();

    }

    public void Hide()
    {
        ui.SetActive(false);
        ShowTurretRange(false);
    }

    public void Upgrade()
    {
        HideUpgradeInfo();
        ShowTurretRange(false);
        target.UpgradeTurret();       
        BuildManager.instance.DeselectTurretNode();
    }

    public void Sell()
    {
        ShowTurretRange(false);
        target.SellTurret();       
        target.isUpgraded = false;
        BuildManager.instance.DeselectTurretNode();
    }

    //circle around the turret indicating range
    void ShowTurretRange (bool cond)
    {
        if (turret != null)
        {
            turret.rangeGO.SetActive(cond);
        }
    }

    private void FillUiInfo()
    {
        if (turret == null)
        {
            return;
        }

        Bullet bullet = turret.bulletPrefab.GetComponent<Bullet>();

        turretNameText.text = turret.gameObject.tag.ToString();
        damageValueText.text = bullet.damage.ToString();
        rangeValueText.text = turret.range.ToString();
        fireRateValueText.text = turret.fireRate.ToString() + "/s";

        switch (turret.gameObject.tag.ToString())
        {
            case "Streličar":
                abilitiesValueText.text = "Nema";
                break;
            case "Bacač raketa":
                abilitiesValueText.text = "AOE";
                break;
            case "Zamrzivač":
                abilitiesValueText.text = "Usporava";
                break;
            case "Snajper":
                abilitiesValueText.text = "Nema";
                break;
        }       
    }

    public void FillUpgradeInfo()
    {
        if (target == null)
        {
            return;
        }

        if (!target.isUpgraded)
        {
            damageValueUpgradeText.text = "+" + GetDamageDiff(target.turretBlueprint);
            rangeValueUpgradeText.text = "+" + GetRangeDiff(target.turretBlueprint);
            fireRateValueUpgradeText.text = "+" + GetFireRateDiff(target.turretBlueprint);

        }
    }

    private string GetFireRateDiff(TurretBlueprint turretBlueprint)
    {
        Turret turret = turretBlueprint.prefab.GetComponent<Turret>();
        Turret upgradedTurret = turretBlueprint.upgradedPrefab.GetComponent<Turret>();

        float fireRateDiff = upgradedTurret.fireRate - turret.fireRate;

        return fireRateDiff.ToString();
    }

    private string GetRangeDiff(TurretBlueprint turretBlueprint)
    {
        Turret turret = turretBlueprint.prefab.GetComponent<Turret>();
        Turret upgradedTurret = turretBlueprint.upgradedPrefab.GetComponent<Turret>();

        float rangeDiff = upgradedTurret.range - turret.range;

        return rangeDiff.ToString();
    }

    private string GetDamageDiff(TurretBlueprint turretBlueprint)
    {
        Bullet bullet = turretBlueprint.prefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>();
        Bullet upgradedBullet = turretBlueprint.upgradedPrefab.GetComponent<Turret>().bulletPrefab.GetComponent<Bullet>();

        int damageDiff = upgradedBullet.damage - bullet.damage;

        return damageDiff.ToString();
    }

    public void HideUpgradeInfo()
    {
        damageValueUpgradeText.text = "";
        rangeValueUpgradeText.text = "";
        fireRateValueUpgradeText.text = "";
    }
}
