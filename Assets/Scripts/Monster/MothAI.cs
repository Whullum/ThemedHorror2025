using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MothAI : MonoBehaviour
{
    public enum MothState
    {
        IDLE,
        WALK,
        CHASE,
        ATTACK
    }

    [SerializeField] private Transform player;
    [SerializeField] private float patrolTime;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float chaseTime;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float noiseDetectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool showDebug;

    private NavMeshAgent agent;
    private Transform target;
    private NavMeshPath path;
    private MothState currentState;
    private MothState previousState;
    private Vector3Int tileSize;
    private bool followPlayer;
    private float stateTime;

    private void Start()
    {
        target = new GameObject("MothTarget").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = MothState.IDLE;
        previousState = MothState.IDLE;
        path = new NavMeshPath();

        GameManager.Instance.MainTilemap.CompressBounds();
        tileSize = GameManager.Instance.MainTilemap.size;

        StartCoroutine(ExecuteBehavior());
    }

    public void DetectPlayer()
    {
        currentState = MothState.CHASE;
    }

    private IEnumerator ExecuteBehavior()
    {
        while (true)
        {
            switch (currentState)
            {
                case MothState.IDLE:
                    IDLE();
                    break;
                case MothState.WALK:
                    Walk();
                    break;
                case MothState.CHASE:
                    Chase();
                    break;
                case MothState.ATTACK:
                    Attack();
                    break;
            }

            stateTime += Time.deltaTime;

            yield return null;
        }
    }

    private void IDLE()
    {
        if (previousState != MothState.IDLE)
        {
            agent.speed = 0.0f;
            agent.isStopped = true;
            stateTime = 0.0f;
            previousState = MothState.IDLE;
        }

        if (stateTime >= patrolTime)
        {
            Vector3 randomPosition = Random.insideUnitCircle * tileSize.x;
            NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 100, 1);

            if (agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                target.position = hit.position;
                agent.SetDestination(target.position);
                currentState = MothState.WALK;
            }
        }
    }

    private void Walk()
    {
        if (previousState != MothState.WALK)
        {
            agent.speed = walkSpeed;
            agent.isStopped = false;
            stateTime = 0.0f;
            previousState = MothState.WALK;
        }

        if (agent.remainingDistance < 1.0f)
        {
            currentState = MothState.IDLE;
        }
    }

    private void Chase()
    {
        if (previousState != MothState.CHASE)
        {
            agent.speed = chaseSpeed;
            agent.isStopped = false;
            stateTime = 0.0f;
            previousState = MothState.CHASE;
        }

        agent.SetDestination(player.position);

        // raycast towards player to see if moth has line of sight, if not then proceed
        // to changue state once the chase time finished

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRadius, playerLayer);
        Debug.Log("inside");

        if (hit.collider != null)
            Debug.Log(hit.collider.name);

        if (showDebug)
        {
            Debug.DrawRay(transform.position, (player.position - transform.position) * detectionRadius, Color.green, 0.1f);
        }

        if (stateTime >= chaseTime && hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            Debug.Log("GOING IDLE");
            currentState = MothState.IDLE;
        }
    }

    private void Attack()
    {
        if (previousState != MothState.ATTACK)
        {
            previousState = MothState.ATTACK;
        }
    }
}