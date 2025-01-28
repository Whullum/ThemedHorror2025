using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private float offset = -90.0f;

    private Vector3 mousePosition;
    private Vector3 direction;
    private float angle;

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;
        direction.z = 0;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }
}
