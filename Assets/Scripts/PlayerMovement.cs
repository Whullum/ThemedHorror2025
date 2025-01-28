using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;

    private Rigidbody2D body;
    private Vector2 movementVector;
    private float horizontalAxisInput;
    private float verticalAxisInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        verticalAxisInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        movementVector = new Vector2(horizontalAxisInput, verticalAxisInput).normalized;
        body.linearVelocity = movementVector * movementSpeed * Time.deltaTime;
    }
}
