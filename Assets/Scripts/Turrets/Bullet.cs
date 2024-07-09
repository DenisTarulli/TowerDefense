using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [SerializeField] private float speed;
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private float hitParticlesDuration;

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
    }

    private void HitTarget()
    {
        GameObject hitEffect = Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(hitEffect, hitParticlesDuration);

        Destroy(gameObject);
    }
}
