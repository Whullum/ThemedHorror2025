using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private float livesRestoreTime;
    [SerializeField] private Material hitShader;
    [Range(0f, 3f)]
    [SerializeField] private float shaderIntensity = 2.5f;
    [SerializeField] private UnityEvent OnPlayerHit;
    [SerializeField] private UnityEvent OnPlayerDeath;

    private int currentLives;
    private float livesTime;
    private bool death;

    private void Awake()
    {
        currentLives = lives;
    }

    private void Update()
    {
        if(currentLives < lives)
        {
            livesTime += Time.deltaTime;

            if (livesTime >= livesRestoreTime)
            {
                currentLives = lives;
                hitShader.SetFloat("_VignettePower", 50.0f);
                livesTime = 0.0f;
            }
        }
    }

    public void TakeHit()
    {
        currentLives--;

        OnPlayerHit?.Invoke();
        GameManager.Instance.PlayerHit();

        hitShader.SetFloat("_VignettePower", shaderIntensity);

        if (currentLives <= 0 && !death)
        {
            death = true;
            OnPlayerDeath?.Invoke();
            GameManager.Instance.PlayerDeath();
        }
    }

    private void OnDestroy()
    {
        hitShader.SetFloat("_VignettePower", 50.0f);
    }
}
