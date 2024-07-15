using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex;

    private Enemy enemy;

    [SerializeField] private float distanceToPointTolerance;

    private void Awake()
    {
        waypointIndex = 0;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[waypointIndex];
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveDirection = target.position - transform.position;
        moveDirection.Normalize();

        transform.Translate(enemy.MoveSpeed * Time.deltaTime * moveDirection);

        if (Vector3.Distance(transform.position, target.position) < distanceToPointTolerance)
        {
            transform.position = target.position;
            GetNextWaypoint();
        }

        enemy.MoveSpeed = enemy.StartingSpeed;
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex == Waypoints.points.Length - 1)
        {
            EndPath();
            Destroy(gameObject);
        }
        else
        {
            waypointIndex++;

            target = Waypoints.points[waypointIndex];
        }
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
