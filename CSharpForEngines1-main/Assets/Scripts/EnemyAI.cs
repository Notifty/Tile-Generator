using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float chaseSpeed = 5f;
    public float detectionRange = 5f;
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;
    private Transform targetPoint;
    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            targetPoint = patrolPoints[currentPointIndex];
        }
        else
        {
            Debug.LogWarning("Patrol points not assigned.");
        }

        player = GameObject.FindWithTag("Player").transform; // Ensure player has the tag "Player"
    }

    void Update()
    {
        if (targetPoint == null || patrolPoints.Length == 0)
            return;

        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            targetPoint = patrolPoints[currentPointIndex];
        }
        MoveTowardsTarget(moveSpeed);
    }

    private void ChasePlayer()
    {
        targetPoint = player;
        MoveTowardsTarget(chaseSpeed);
    }

    private void MoveTowardsTarget(float speed)
    {
        if (targetPoint == null)
            return;

        Vector2 direction = (targetPoint.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
    }
}
