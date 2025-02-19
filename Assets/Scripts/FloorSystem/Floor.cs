using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform PlayerSpawnPoint { get { return playerSpawnPoint; } }
    public Transform MonsterSpawnPoint { get { return monsterSpawnPoint; } }

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform monsterSpawnPoint;
}
