using UnityEngine;
using UnityEngine.Events;

public class NoiseDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent OnNoiseDetected;

    public void Trigger()
    {
        OnNoiseDetected?.Invoke();
    }
}
