using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

    [Header("IDLE STATE")]
    [Tooltip("Time until the monster starts moving again to a new point.")]
    [SerializeField] private float idleTime;
    [Header("WALK STATE")]
    [SerializeField] private float walkSpeed;
    [Header("CHASE STATE")]
    [Tooltip("Time until the monster losses interest in the player once it was detected " +
        "(only if no direct line of sight towards the player).")]
    [SerializeField] private float chaseTime;
    [SerializeField] private float chaseSpeed;
    [Tooltip("Distance at which the player will be detected independently if the light is pointed towards the monster.")]
    [SerializeField] private float detectionRadius;
    [Header("ATTACK STATE")]
    [Tooltip("Time between attacks.")]
    [SerializeField] private float attackSpeed;
    [Tooltip("Minimum distance between the player and the monster for the monster to be able to hit the player.")]
    [SerializeField] private float attackDistance;
    [Header("Other Settings")]
    [SerializeField] private LayerMask visibleLayers;
    [SerializeField] private bool showDebug;
    [Header("Events")]
    [SerializeField] private UnityEvent OnEnterIDLEState;
    [SerializeField] private UnityEvent OnEnterWalkState;
    [SerializeField] private UnityEvent OnEnterChaseState;
    [SerializeField] private UnityEvent OnEnterAttackState;
    [SerializeField] private UnityEvent OnHitPlayer;

    private NavMeshAgent agent;
    private Transform target;
    private NavMeshPath path;
    private MothState currentState;
    private MothState previousState;
    private Vector3Int tileSize;
    private bool followPlayer;
    private bool playerHit;
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
            OnEnterIDLEState?.Invoke();
        }

        if (stateTime >= idleTime)
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
            OnEnterWalkState?.Invoke();
        }

        if (agent.remainingDistance < 1.0f)
        {
            currentState = MothState.IDLE;
        }
        else if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            currentState = MothState.CHASE;
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
            OnEnterChaseState?.Invoke();
        }

        agent.SetDestination(player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRadius, visibleLayers);

        if (showDebug)
        {
            Debug.DrawRay(transform.position, (player.position - transform.position) * detectionRadius, Color.green, 0.1f);
        }

        if (stateTime >= chaseTime && hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            currentState = MothState.IDLE;
        }
        else if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            currentState = MothState.ATTACK;
        }
    }

    private void Attack()
    {
        if (previousState != MothState.ATTACK)
        {
            agent.isStopped = true;
            previousState = MothState.ATTACK;
            OnEnterAttackState?.Invoke();
        }

        if (playerHit)
        {
            if (stateTime >= attackSpeed)
            {
                playerHit = false;
                currentState = MothState.CHASE;
            }
        }
        else
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackDistance, transform.right, visibleLayers);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null)
                {
                    if (hits[i].collider.TryGetComponent(out PlayerController controller))
                    {
                        controller.TakeHit();
                        playerHit = true;
                        OnHitPlayer?.Invoke();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showDebug)
        {
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}