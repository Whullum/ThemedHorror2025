using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject Player { get { return player; } }
    public GameObject Monster { get { return monster; } }
    public Tilemap MainTilemap { get { return tilemap; } }

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monster;
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
        UIManager.Instance.ToggleGameOverScreen(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("BuildLevel");
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }
}
