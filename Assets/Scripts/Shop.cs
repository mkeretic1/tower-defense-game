using System;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Turrets")]
    public TurretBlueprint arrowTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint slowingTurret;
    public TurretBlueprint ballistaTurret;

    [Header("Turret hover info")]
    public GameObject turretInfoUI;
    public float positionOffset = 180f;
    public Text turretNameText;
    public Text damageValueText;
    public Text rangeValueText;
    public Text fireRateValueText;
    public Text abilitiesValueText;
    public Text descriptionText;
    public Text descriptionValueText;

    public GameObject arrowTurretButton;
    public GameObject missileLauncherButton;
    public GameObject slowingTurretButton;
    public GameObject ballistaTurretButton;

    [Header("Cost text")]
    public Text arrowTurretCostText;
    public Text missileLauncherCostText;
    public Text slowingTurretCostText;
    public Text ballistaTurretCostText;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        arrowTurretCostText.text = arrowTurret.cost.ToString();
        missileLauncherCostText.text = missileLauncher.cost.ToString();
        slowingTurretCostText.text = slowingTurret.cost.ToString();
        ballistaTurretCostText.text = ballistaTurret.cost.ToString();

        descriptionText.enabled = false;
        descriptionValueText.enabled = false;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectArrowTurret();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectMissileLauncher();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectSlowingTurret();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SelectBallistaTurret();
        }
        
    }

    public void SelectArrowTurret()
    {
        buildManager.SelectTurretToBuild(arrowTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectSlowingTurret()
    {
        buildManager.SelectTurretToBuild(slowingTurret);
    }

    public void SelectBallistaTurret()
    {
        buildManager.SelectTurretToBuild(ballistaTurret);
    }

    public void ShowArrowTurretInfo()
    {
        FillTurretInfo(arrowTurret, arrowTurretButton, "None");
    }

    public void ShowMissileLauncherInfo()
    {   
        FillTurretInfo(missileLauncher, missileLauncherButton, "AOE");
    }

    public void ShowSlowingTurretInfo()
    {
        FillTurretInfo(slowingTurret, slowingTurretButton, "Slows");
    }

    public void ShowBallistaTurretInfo()
    {
        FillTurretInfo(ballistaTurret, ballistaTurretButton, "None");
    }

    public void HideTurretInfo()
    {
        turretInfoUI.SetActive(false);
    }

    private void FillTurretInfo(TurretBlueprint t, GameObject turretButton, string abilitiesText)
    {
        turretInfoUI.SetActive(true);
        turretInfoUI.transform.position = turretButton.transform.position + new Vector3(0f, positionOffset, 0f);
        Turret turret = t.prefab.GetComponent<Turret>();
        Bullet bullet = turret.bulletPrefab.GetComponent<Bullet>();

        turretNameText.text = turret.gameObject.tag.ToString();
        damageValueText.text = bullet.damage.ToString();
        rangeValueText.text = turret.range.ToString();
        fireRateValueText.text = turret.fireRate.ToString() + "/s";
        abilitiesValueText.text = abilitiesText;
    }
}
