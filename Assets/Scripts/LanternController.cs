using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LanternController : MonoBehaviour
{
    [Tooltip("Layer of the monster. Used to detect it using raycast.")]
    [SerializeField] private LayerMask monsterLayer;
    [SerializeField] private UnityEvent onMonsterDetected;
    [SerializeField] private UnityEvent onMonsterUndetected;
    [SerializeField] private bool showDebug;

    private Light2D lanternLight;
    private Coroutine flickerCoroutine;
    private RaycastHit2D hit;
    private bool monsterDetected;
    private float initialIntensity;

    private void Awake()
    {
        lanternLight = GetComponent<Light2D>();
        initialIntensity = lanternLight.intensity;
    }

    private void Update()
    {
        CheckTargets();
    }

    private void CheckTargets()
    {
        hit = Physics2D.Raycast(transform.position, transform.up, lanternLight.pointLightOuterRadius, monsterLayer);

        if (hit.collider != null)
        {
            if (!monsterDetected)
            {
                if (flickerCoroutine != null)
                {
                    StopCoroutine(flickerCoroutine);
                }

                flickerCoroutine = StartCoroutine(FlickerLight());
                onMonsterDetected?.Invoke();
            }

            monsterDetected = true;
        }
        else
        {
            if (monsterDetected)
            {
                onMonsterUndetected?.Invoke();
            }

            monsterDetected = false;
        }

        if (showDebug)
        {
            Debug.DrawRay(transform.position, transform.up * lanternLight.pointLightOuterRadius, Color.red);
        }
    }

    private IEnumerator FlickerLight()
    {
        yield return null;

        float rndIntesity = 0.0f;

        while (monsterDetected)
        {
            rndIntesity = Random.Range(0.0f, 1.0f);

            lanternLight.intensity = rndIntesity;

            yield return null;
        }

        lanternLight.intensity = initialIntensity;
    }
}
