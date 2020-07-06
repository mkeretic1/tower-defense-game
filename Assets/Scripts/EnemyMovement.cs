using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    public Vector3 positionOffset;

    private Enemy enemy;

    public bool isFlyingEnemy = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        if (isFlyingEnemy)
        {
            target = Waypoints.points[Waypoints.points.Length - 1];
        }
        else
        {
            target = Waypoints.points[0];
        }

    }

    void Update()
    {
        Vector3 direction = target.position + positionOffset - transform.position + positionOffset;
        transform.Translate((direction.normalized) * enemy.speed * Time.deltaTime, Space.World);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float step = 360 * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            if (isFlyingEnemy)
            {
                CastleReached();
                return;
            }
            else
            {
                GetNextWaypoint();
            }
        }
    }


    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            CastleReached();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void CastleReached()
    {
        PlayerStats.lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
