using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAI : MonoBehaviour
{
    [SerializeField] private Transform player;

    private NavMeshAgent agent;
    private bool followPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!followPlayer)
        {
            return;
        }

        agent.SetDestination(player.position);
    }

    public void FollowPlayer(bool follow)
    {
        followPlayer = follow;
    }
}
