using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClosed;

    public void ToggleDoor()
    {
        doorOpen.SetActive(!doorOpen.activeSelf);
        doorClosed.SetActive(!doorClosed.activeSelf);
    }
}
