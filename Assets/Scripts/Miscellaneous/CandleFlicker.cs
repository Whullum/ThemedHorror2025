using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class CandleFlicker : MonoBehaviour
{
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.0f;
    [SerializeField] private float speed = 1.0f;

    private Light2D candleLight;

    private void Awake()
    {
        candleLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        float intensity = Random.Range(minIntensity, maxIntensity);
        float currentIntesity = Mathf.Lerp(candleLight.intensity, intensity, speed * Time.deltaTime);
        candleLight.intensity = currentIntesity;
    }
}
