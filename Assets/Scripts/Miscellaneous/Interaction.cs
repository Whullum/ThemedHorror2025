using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [Header("Events")]
    [SerializeField] private UnityEvent OnInteractionExecuted;
    [SerializeField] private UnityEvent OnEnterInteraction;
    [SerializeField] private UnityEvent OnExitInteraction;
    [SerializeField] private UnityEvent OnMonsterEnter;

    private bool canInteract;

    private void Update()
    {
        if (!canInteract)
        {
            return;
        }

        if (Input.GetKeyDown(interactionKey))
        {
            OnInteractionExecuted?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnEnterInteraction?.Invoke();
            canInteract = true;
        }
        else if (collision.CompareTag("Monster"))
        {
            OnMonsterEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnExitInteraction?.Invoke();
            canInteract = false;
        }
        else if (collision.CompareTag("Monster"))
        {
            OnMonsterEnter?.Invoke();
        }
    }
}
