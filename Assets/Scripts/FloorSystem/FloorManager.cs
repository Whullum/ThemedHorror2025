using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance { get { return instance; } }

    [Tooltip("Floors in order. 0 = first floor in the list. 1 = second floor on the list...")]
    [SerializeField] Floor[] floors;

    private static FloorManager instance;
    private int currentFloor;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void GoToFloor(int floorNumber)
    {
        for (int i = 0; i < floors.Length; i++)
        {
            if (i == floorNumber)
            {
                floors[i].gameObject.SetActive(true);
                currentFloor = floorNumber;
                GameManager.Instance.Player.transform.position = floors[i].PlayerSpawnPoint.position;
                GameManager.Instance.Monster.transform.position = floors[i].MonsterSpawnPoint.position;
            }
            else
            {
                floors[i].gameObject.SetActive(false);
            }
        }
    }
}
