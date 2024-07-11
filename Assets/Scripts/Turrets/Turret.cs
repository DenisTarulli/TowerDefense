using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float range;

    [Header("Use Bullets (default)")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    private float nextTimeToShoot;

    [Header("Use Laser")]
    [SerializeField] private int damageOverTime;
    [SerializeField] private float slowAmount;
    [SerializeField] private bool useLaser = false;
    [SerializeField] private ParticleSystem impactEffect;
    private Light impactLight;
    private LineRenderer lineRenderer;

    [Header("Setup fields")]
    [SerializeField] private Transform partToRotate;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float searchTargetRepeatRate;

    private Transform target;
    private Enemy targetEnemy;

    private const string IS_ENEMY = "Enemy";

    private void Start()
    {
        if (useLaser)
        {
            lineRenderer = GetComponent<LineRenderer>();
            impactLight = GetComponentInChildren<Light>();
        }

        InvokeRepeating(nameof(UpdateTarget), 0f, searchTargetRepeatRate);
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }

            return;
        }

        RotateTurret();

        if (useLaser)
        {
            Laser();
        }
        else
            Shoot();        
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(IS_ENEMY);

        float longestLifeTime = 0f;
        float distanceToEnemy = Mathf.Infinity;
        GameObject firstEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float lifeTime = enemy.GetComponent<Enemy>().LifeTime;

            if (lifeTime > longestLifeTime)
            {
                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy <= range)
                {
                    longestLifeTime = lifeTime;
                    firstEnemy = enemy;
                }
            }
        }

        if (firstEnemy != null && distanceToEnemy <= range)
        {
            target = firstEnemy.transform;
            targetEnemy = firstEnemy.GetComponent<Enemy>();
        }
        else 
            target = null;
    }

    private void RotateTurret()
    {
        Vector3 targetDirection = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - target.position;
        
        impactEffect.transform.SetPositionAndRotation(target.position + direction.normalized * 1.25f, Quaternion.LookRotation(direction));
    }

    private void Shoot()
    {
        if (!(nextTimeToShoot <= Time.time)) return;

        nextTimeToShoot = Time.time + 1f / fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (bullet.TryGetComponent<Bullet>(out Bullet component))
            component.SetTarget(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
