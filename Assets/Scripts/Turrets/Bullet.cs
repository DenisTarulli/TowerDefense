using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float speed;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private float hitParticlesDuration;

    private const string IS_ENEMY = "Enemy";

    public void SetTarget(Transform targetToSet)
    {
        target = targetToSet;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        GameObject hitEffect = Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(hitEffect, hitParticlesDuration);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitObject in hitObjects)
        {
            if (hitObject.gameObject.CompareTag(IS_ENEMY))
                Damage(hitObject.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
