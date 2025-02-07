using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private UnityEvent OnPlayerHit;
    [SerializeField] private UnityEvent OnPlayerDeath;

    public void TakeHit()
    {
        lives--;

        OnPlayerHit?.Invoke();

        if (lives <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }
}
