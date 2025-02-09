using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float runSpeed = 20.0f;
    [SerializeField] private float runningMakesNoiseTime = 2.0f;
    [SerializeField] private UnityEvent OnStartRunning;
    [SerializeField] private UnityEvent OnStopRunning;
    [SerializeField] private UnityEvent OnMakingNoise;

    private Rigidbody2D body;
    private Vector2 movementVector;
    private bool running;
    private float horizontalAxisInput;
    private float verticalAxisInput;
    private float currentSpeed;
    private float runTime;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        verticalAxisInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && currentSpeed != runSpeed)
        {
            currentSpeed = runSpeed;
            OnStartRunning?.Invoke();
            running = true;
            runTime = 0.0f;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && currentSpeed != walkSpeed)
        {
            currentSpeed = walkSpeed;
            OnStopRunning?.Invoke();
            running = false;
        }

        if (running)
        {
            if (runTime >= runningMakesNoiseTime)
            {
                OnMakingNoise?.Invoke();
            }

            runTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        movementVector = new Vector2(horizontalAxisInput, verticalAxisInput).normalized;
        body.linearVelocity = movementVector * currentSpeed * Time.deltaTime;
    }
}
