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
        GameManager.Instance.PlayerHit();

        if (lives <= 0)
        {
            OnPlayerDeath?.Invoke();
            GameManager.Instance.PlayerDeath();
        }
    }
}
