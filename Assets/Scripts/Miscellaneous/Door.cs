using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClosed;
    [SerializeField] private UnityEvent onDoorOpen;
    [SerializeField] private UnityEvent onDoorClosed;

    public void ToggleDoor()
    {
        doorOpen.SetActive(!doorOpen.activeSelf);
        doorClosed.SetActive(!doorClosed.activeSelf);

        if(doorOpen.activeSelf)
        {
            onDoorOpen?.Invoke();
        }
        else
        {
            onDoorClosed?.Invoke();
        }
    }

    public void OpenDoor()
    {
        doorClosed.SetActive(false);
        doorOpen.SetActive(true);

        if (doorOpen.activeSelf)
        {
            onDoorOpen?.Invoke();
        }
        else
        {
            onDoorClosed?.Invoke();
        }
    }

    public void CloseDoor()
    {
        doorClosed.SetActive(true);
        doorOpen.SetActive(false);

        if (doorOpen.activeSelf)
        {
            onDoorOpen?.Invoke();
        }
        else
        {
            onDoorClosed?.Invoke();
        }
    }
}
