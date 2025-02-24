using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10.0f;
    [SerializeField] private float runSpeed = 20.0f;
    [SerializeField] private float runningMakesNoiseTime = 2.0f;
    [SerializeField] private Transform lantern;
    [SerializeField] private Transform[] lanternPositions; // Positions depending on player movement direction, in order: top, bottom, left, right.
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

    // Player Animation -Will
    private Animator playerAnimator;
    private const string horizontalAnim = "Horizontal";
    private const string verticalAnim = "Vertical";
    private const string idleHorizontalAnim = "Idle_Horiz";
    private const string idleVerticalAnim = "Idle_Vert";

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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

        playerAnimator.SetFloat(horizontalAnim, movementVector.x);
        playerAnimator.SetFloat(verticalAnim, movementVector.y);


        if (movementVector.y > 0.0f)
        {
            lantern.position = lanternPositions[0].position;
        }
        else if (movementVector.y < 0.0f)
        {
            lantern.position = lanternPositions[1].position;
        }
        else if (movementVector.x > 0.0f)
        {
            lantern.position = lanternPositions[3].position;
        }
        else if (movementVector.x < 0.0f)
        {
            lantern.position = lanternPositions[2].position;
        }

        if (movementVector == Vector2.zero)
        {
            if (lantern.position == lanternPositions[0].position)
            {
                playerAnimator.SetFloat(idleHorizontalAnim, 0);
                playerAnimator.SetFloat(idleVerticalAnim, 1);
            }
            else if (lantern.position == lanternPositions[1].position)
            {
                playerAnimator.SetFloat(idleHorizontalAnim, 0);
                playerAnimator.SetFloat(idleVerticalAnim, -1);
            }
            else if (lantern.position == lanternPositions[3].position)
            {
                playerAnimator.SetFloat(idleHorizontalAnim, 1);
                playerAnimator.SetFloat(idleVerticalAnim, 0);
            }
            else if (lantern.position == lanternPositions[2].position)
            {
                playerAnimator.SetFloat(idleHorizontalAnim, -1);
                playerAnimator.SetFloat(idleVerticalAnim, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        movementVector = new Vector2(horizontalAxisInput, verticalAxisInput).normalized;
        body.linearVelocity = movementVector * currentSpeed * Time.deltaTime;
    }
}
