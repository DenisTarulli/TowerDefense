using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartTurret : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float range;
    [SerializeField] private float searchTargetRepeatRate;

    [SerializeField] private Transform partToRotate;
    [SerializeField] private float rotationSpeed;

    private const string IS_ENEMY = "Enemy";

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, searchTargetRepeatRate);
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 targetDirection = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(IS_ENEMY);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
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
        }
        else 
            target = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
