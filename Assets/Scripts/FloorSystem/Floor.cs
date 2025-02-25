using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform PlayerSpawnPoint { get { return playerSpawnPoint; } }
    public Transform MonsterSpawnPoint { get { return monsterSpawnPoint; } }
    public bool MonsterActive { get { return monsterActive; } }
    public float MonsterSpeed { get { return monsterSpeed; } }

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform monsterSpawnPoint;
    [SerializeField] private bool monsterActive;
    [SerializeField] private float monsterSpeed;
}
