using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform PlayerSpawnPoint { get { return playerSpawnPoint; } }
    public Transform MonsterSpawnPoint { get { return monsterSpawnPoint; } }
    public bool MonsterActive { get { return monsterActive; } }

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform monsterSpawnPoint;
    [SerializeField] private bool monsterActive;
}
