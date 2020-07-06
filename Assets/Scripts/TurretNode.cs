using UnityEngine;
using UnityEngine.EventSystems;


public class TurretNode : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    //[HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;


   void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.GetColor("_Color");

        buildManager = BuildManager.instance;
    }


    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        

        if (turret != null)
        {
            buildManager.SelectTurretNode(this);
            return;
        }

        if (!buildManager.CanBuild())
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
        buildManager.deselectPreviewPrefab();

        
    }

    void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
        {
            GameObject noGoldUI = Instantiate(buildManager.notEnoughMoneyUI, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
            Destroy(noGoldUI, 5f);
            return;
        }

        PlayerStats.money -= blueprint.cost;

        GameObject turret = (GameObject)Instantiate(blueprint.prefab, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
        this.turret = turret;

        //when built, hide range indicator
        turret.GetComponentInChildren<Turret>().rangeGO.SetActive(false);

        //enableShooting
        this.turret.GetComponentInChildren<Turret>().setIsPlacedTrue();

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
        Destroy(effect, 5f);

        //spawn gold fadeaway after turret is placed
        buildManager.gainLoseGoldUI.goldFadeawayCanvas(transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset), ("-" + turretBlueprint.cost.ToString()));
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            GameObject noGoldUI = Instantiate(buildManager.notEnoughMoneyUI, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
            Destroy(noGoldUI, 5f);
            return;
        }

        PlayerStats.money -= turretBlueprint.upgradeCost;

        Destroy(this.turret);

        GameObject turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
        this.turret = turret;

        //when built, hide range indicator
        turret.GetComponentInChildren<Turret>().rangeGO.SetActive(false);

        //enableShooting
        this.turret.GetComponentInChildren<Turret>().setIsPlacedTrue();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
        Destroy(effect, 5f);

        //spawn gold fadeaway after turret is upgraded
        buildManager.gainLoseGoldUI.goldFadeawayCanvas(transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset), ("-" + turretBlueprint.upgradeCost.ToString()));

        isUpgraded = true;
    }

    public void SellTurret()
    {
        if (!isUpgraded)
        {
            PlayerStats.money += turretBlueprint.GetSellAmount();
        }
        else
        {
            PlayerStats.money += turretBlueprint.GetUpgradedSellAmount();
        }
        

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset));
        Destroy(effect, 5f);

        //spawn gold fadeaway after turret is sold
        if (!isUpgraded)
        {
            buildManager.gainLoseGoldUI.goldFadeawayCanvas(transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset), ("+" + turretBlueprint.GetSellAmount().ToString()));
        }
        else
        {
            buildManager.gainLoseGoldUI.goldFadeawayCanvas(transform.position + positionOffset, transform.rotation * Quaternion.Euler(rotationOffset), ("+" + turretBlueprint.GetUpgradedSellAmount().ToString()));
        }     

        Destroy(turret);

        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild())
        {
            return;
        }

        if (buildManager.HasEnoughMoney())
        {
            rend.material.SetColor("_Color", hoverColor);
        }
        else
        {
            rend.material.SetColor("_Color", notEnoughMoneyColor);
        }
    }

    void OnMouseExit()
    {
        rend.material.SetColor("_Color", startColor);
    }
}
