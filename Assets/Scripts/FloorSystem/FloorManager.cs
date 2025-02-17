using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance { get { return instance; } }

    [Tooltip("Floors in order. 0 = first floor in the list. 1 = second floor on the list...")]
    [SerializeField] GameObject[] floors;

    private static FloorManager instance;
    private int currentFloor;

    private void Awake()
    {
        if (instance == null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void GoToFloor(int floorNumber)
    {
        currentFloor = floorNumber;

        for (int i = 0; i < floors.Length; i++)
        {
            if (currentFloor == floorNumber)
            {
                floors[i].gameObject.SetActive(true);
            }
            else
            {
                floors[i].gameObject.SetActive(false);
            }
        }
    }
}
