using System;
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
    private RaycastHit2D[] hits = new RaycastHit2D[5];
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
        Vector2 rightVector = CalculateVectorDirection(transform.up, lanternLight.pointLightOuterAngle / 2);
        Vector2 rightMiddleVector = CalculateVectorDirection(transform.up, lanternLight.pointLightOuterAngle / 4);
        Vector2 leftVector = CalculateVectorDirection(transform.up, -lanternLight.pointLightOuterAngle / 2);
        Vector2 leftMiddleVector = CalculateVectorDirection(transform.up, -lanternLight.pointLightOuterAngle / 4);

        hits[0] = Physics2D.Raycast(transform.position, transform.up, lanternLight.pointLightOuterRadius, monsterLayer);
        hits[1] = Physics2D.Raycast(transform.position, rightVector, lanternLight.pointLightOuterRadius, monsterLayer);
        hits[2] = Physics2D.Raycast(transform.position, rightMiddleVector, lanternLight.pointLightOuterRadius, monsterLayer);
        hits[3] = Physics2D.Raycast(transform.position, leftVector, lanternLight.pointLightOuterRadius, monsterLayer);
        hits[4] = Physics2D.Raycast(transform.position, leftMiddleVector, lanternLight.pointLightOuterRadius, monsterLayer);

        if (showDebug)
        {
            Debug.DrawRay(transform.position, transform.up * lanternLight.pointLightOuterRadius, Color.red);
            Debug.DrawRay(transform.position, rightVector * lanternLight.pointLightOuterRadius, Color.red);
            Debug.DrawRay(transform.position, rightMiddleVector * lanternLight.pointLightOuterRadius, Color.red);
            Debug.DrawRay(transform.position, leftVector * lanternLight.pointLightOuterRadius, Color.red);
            Debug.DrawRay(transform.position, leftMiddleVector * lanternLight.pointLightOuterRadius, Color.red);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider == null)
            {
                continue;
            }

            if (!monsterDetected)
            {
                if (flickerCoroutine != null)
                {
                    StopCoroutine(flickerCoroutine);
                }

                monsterDetected = true;
                flickerCoroutine = StartCoroutine(FlickerLight());
                onMonsterDetected?.Invoke();

                return;
            }

            return;
        }

        if (monsterDetected)
        {
            onMonsterUndetected?.Invoke();
        }

        monsterDetected = false;
    }

    private Vector2 CalculateVectorDirection(Vector3 aVector, float angle)
    {
        float radians = angle * Mathf.PI / 180;
        double Bx = aVector.x * Math.Cos(radians) - aVector.y * Math.Sin(radians);
        double By = aVector.x * Math.Sin(radians) + aVector.y * Math.Cos(radians);

        return new Vector2((float)Bx, (float)By);
    }

    private IEnumerator FlickerLight()
    {
        float rndIntesity = 0.0f;

        while (monsterDetected)
        {
            rndIntesity = UnityEngine.Random.Range(0.0f, 1.0f);

            lanternLight.intensity = rndIntesity;

            yield return null;
        }

        lanternLight.intensity = initialIntensity;
    }
}