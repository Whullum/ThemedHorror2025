using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private KeyCode interactionkey;
    [Header("Events")]
    [SerializeField] private UnityEvent OnInteractionExecuted;
    [SerializeField] private UnityEvent OnEnterInteraction;
    [SerializeField] private UnityEvent OnExitInteraction;

    private bool canInteract;

    private void Update()
    {
        if (!canInteract)
        {
            return;
        }

        if (Input.GetKeyDown(interactionkey))
        {
            OnInteractionExecuted?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        OnEnterInteraction?.Invoke();
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        OnExitInteraction?.Invoke();
        canInteract = false;
    }
}
