using UnityEngine;
using UnityEngine.Events;

public class ConfigurationItemHolder : MonoBehaviour
{
    [SerializeField] private ConfigurationItem item;
    [SerializeField] private UnityEvent OnItemActivated;
    [SerializeField] private UnityEvent OnItemDeactivated;

    public bool activated;

    public void ToggleItem()
    {
        activated = !activated;

        if (activated)
        {
            item.ActivateItem();
            OnItemActivated?.Invoke();
        }
        else
        {
            item.DeactivateItem();
            OnItemDeactivated?.Invoke();
        }
    }
}
