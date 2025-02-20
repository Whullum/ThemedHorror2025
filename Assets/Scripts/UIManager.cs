using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get { return instance; } }

    [SerializeField] private GameObject gameOverScreen;

    private static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void ToggleGameOverScreen(bool toggle)
    {
        gameOverScreen.SetActive(toggle);
    }
}
