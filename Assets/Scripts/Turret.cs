using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy = null;

    private EnemyBodyColor bodyColor;

    [Header("Attributes")]
    public float range = 4f;
    public float fireRate = 1f;
    private float shootRecharge = 0.1f;

    [Header("Bonus attributes")]
    public bool slow = false;
    public float slowPercentage = 0.5f;


    [Header("Unity setup fields")]
    public string enemyTag = "Enemy";

    public float turnSpeed = 10;

    public Transform partToRotate;
    public Transform firePoint;
    public GameObject bulletPrefab;

    //stopPreviewPrefabFromShooting
    private bool isPlaced;

    //show range of a turret
    public GameObject rangeGO;

    void Awake()
    {
        isPlaced = false;
    }

    void Start()
    {       
        InvokeRepeating("UpdateTarget", 0f, 0.5f);       
    }

    void UpdateTarget()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= range)
        {
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {          
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced)
        {          
            return;
        }

        if (target == null)
        {
            return;
        }

        LockOnTarget();

        if (shootRecharge <= 0f)
        {
            Shoot();

            if (slow)
            {
                targetEnemy.SlowDownEnemy(slowPercentage);
            }

            shootRecharge = 1f / fireRate;
        }

        shootRecharge -= Time.deltaTime;
    }


    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {

       GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
       Bullet bullet = bulletGO.GetComponent<Bullet>();

       if (bullet != null)
        {
            bullet.Seek(target);
        } 

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void setIsPlacedTrue()
    {
        isPlaced = true;
    }
}
