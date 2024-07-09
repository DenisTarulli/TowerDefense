using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToPointTolerance;

    private Transform target;
    private int waypointIndex;
    private float lifeTime;

    public float LifeTime { get => lifeTime; }

    private void Awake()
    {
        waypointIndex = 0;
        lifeTime = 0f;
    }

    private void Start()
    {
        target = Waypoints.points[waypointIndex];
    }

    private void Update()
    {
        Vector3 moveDirection = target.position - transform.position;
        moveDirection.Normalize();

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);

        lifeTime += Time.deltaTime * moveSpeed;

        if (Vector3.Distance(transform.position, target.position) < distanceToPointTolerance)
        {
            transform.position = target.position;
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex == Waypoints.points.Length - 1)
            Destroy(gameObject);
        else
        {
            waypointIndex++;

            target = Waypoints.points[waypointIndex];
        }        
    }
}
