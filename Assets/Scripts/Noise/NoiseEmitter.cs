using UnityEngine;

public class NoiseEmitter : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out NoiseDetector detector))
        {
            detector.Trigger();
        }
    }
}
