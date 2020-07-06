using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private TurretNode selectedTurretNode;

    //show preview of turret when selected
    private BuildPreview buildPreview = new BuildPreview();
    private GameObject turretPreviewPrefab;

    // ui for upgrade / sell
    public TurretSelectUI turretSelectUI;

    //Gold gain/lose UI stuff
    public GameObject goldFadeawayUI;
    public GameObject notEnoughMoneyUI;
    [HideInInspector]
    public GainLoseGoldUI gainLoseGoldUI;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;

        gainLoseGoldUI = goldFadeawayUI.GetComponent<GainLoseGoldUI>();
    }

    public bool CanBuild()
    {
        return turretToBuild != null;
    }

    public bool HasEnoughMoney()
    {
        return PlayerStats.money >= turretToBuild.cost;
    }


    public void SelectTurretNode(TurretNode turretNode)
    {
        if( selectedTurretNode == turretNode)
        {
            DeselectTurretNode();
            return;
        }

        selectedTurretNode = turretNode;
        deselectPreviewPrefab();

        turretSelectUI.SetTarget(turretNode);
        
    }

    public void DeselectTurretNode()
    {
        selectedTurretNode = null;
        turretSelectUI.Hide();
    }

    public void SelectTurretToBuild (TurretBlueprint turret)
    {      
        if (turretPreviewPrefab != null)
        {
            Destroy(turretPreviewPrefab);
        }

        turretToBuild = turret;

        turretPreviewPrefab = (GameObject) Instantiate(turretToBuild.prefab, new Vector3(0,0,0), Quaternion.identity);
        DeselectTurretNode();
    }

    public TurretBlueprint GetTurretToBuild ()
    {
        return turretToBuild;
    }

    void Update()
    {
        if (turretPreviewPrefab != null)
        {
            buildPreview.turretBuildPreview(turretPreviewPrefab);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (turretPreviewPrefab != null)
            {
                deselectPreviewPrefab();
            }

            DeselectTurretNode();
            turretSelectUI.Hide();
        }
    }

    public void deselectPreviewPrefab()
    {      
        Destroy(turretPreviewPrefab);
        turretToBuild = null;     
    }

    
}
