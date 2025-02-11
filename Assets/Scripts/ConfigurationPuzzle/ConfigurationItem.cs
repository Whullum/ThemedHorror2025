using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigurationItem", menuName = "ConfigurationPuzzle/ConfigurationItem")]
public class ConfigurationItem : ScriptableObject
{
    public Action<ConfigurationItem> OnItemActivated { get; set; }
    public Action<ConfigurationItem> OnItemDeactivated { get; set; }
    public bool Activated { get { return activated; } }

    private bool activated;

    public void ActivateItem()
    {
        OnItemActivated?.Invoke(this);
        activated = true;
    }

    public void DeactivateItem()
    {
        OnItemDeactivated?.Invoke(this);
        activated = false;
    }
}
