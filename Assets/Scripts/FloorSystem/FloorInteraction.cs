using UnityEngine;

public class FloorInteraction : MonoBehaviour
{
    [SerializeField] private int floorNumber;

    public void Transition()
    {
        FloorManager.Instance.GoToFloor(floorNumber);
    }
}
