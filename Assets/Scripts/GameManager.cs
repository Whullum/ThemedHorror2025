using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Tilemap MainTilemap { get { return tilemap; } }

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private UnityEvent OnPlayerDeath;
    [SerializeField] private UnityEvent OnPlayerHit;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void SetMothTarget(Transform target)
    {

    }

    public void PlayerHit()
    {
        OnPlayerHit?.Invoke();
    }

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
        ResetLevel();
    }

    public void ResetLevel()
    {
        //SceneManager.LoadScene();
    }
}
