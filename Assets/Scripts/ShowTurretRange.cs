using UnityEngine;

public class ShowTurretRange : MonoBehaviour
{
    public GameObject turretGO;
    public GameObject rangeUI;
    private Turret turret;
    private float range;


    void Start()
    {
        getTurretRange();
    }

    void getTurretRange()
    {
        turret = turretGO.GetComponent<Turret>();
        range = turret.range * 2;

        Vector3 scale = new Vector3(range, 1f, range);

        this.transform.localScale *= range;
    }
}
