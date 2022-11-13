using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharMovement : MonoBehaviour
{
    // <------------------------------- MOVEMENT ------------------------------- //
    [SerializeField]
    private float movementSpeed = 1.0f;
    [SerializeField]
    private float jumpVelocity = 10.0f;
    [SerializeField, Range(.0f, 1.0f)]
    private float airTurnSpeed = 0.5f;

    private bool movementPressed;
    private bool isJumpPresssed;

    private Vector2 lateralAirVelocity; // Current velocity in the X,Z plane
    private Vector3 inputVelocity; // Movement keys input in X,Z plane (0-1f)
    private CharacterController charController;

    private bool movementKeyPressed = false;

    private float currentRotation;


    // ------------------------------- PHYSICS ------------------------------- //
    [SerializeField]
    private float gravity = -5.0f;

    private float groundedGravity = -0.5f;


    // ------------------------------- ANIMATION ------------------------------- //
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int forward;
    int right;


    // Start is called before the first frame update
    void Start()
    {
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        forward = Animator.StringToHash("forward");
        right = Animator.StringToHash("right");
    }

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovementAnims();
        HandleGravity();
        HandleMovement();
    }

    public void HandleMovement()
    {
        var lateralVelocity = new Vector2(inputVelocity.x, inputVelocity.z).normalized;
        if (charController.isGrounded) {   
            lateralAirVelocity = new Vector2(inputVelocity.x, inputVelocity.z).normalized;
            //Stop movement after key canceled
            if (!movementKeyPressed) {
                lateralVelocity = new Vector2(0.0f, 0.0f); 
            }
        } else {
            lateralVelocity = Vector2.Lerp(lateralAirVelocity, lateralVelocity, airTurnSpeed);
        }
        lateralVelocity *= movementSpeed;
        charController.Move(new Vector3(lateralVelocity.x, inputVelocity.y, lateralVelocity.y) * Time.deltaTime);
    }

    public void HandleGravity()
    {
        if (charController.isGrounded) {
            if (inputVelocity.y > -20.0f) {
                inputVelocity.y += groundedGravity * Time.deltaTime;
            }
        } else {
            inputVelocity.y += gravity * Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) // TODO: these events run 2-3 times on click consider doing only once through logic
    {
        var incVelocity = ctx.ReadValue<Vector2>().normalized;
        if(ctx.canceled) {
            movementKeyPressed = false;
        } 
        if(ctx.started) {
           movementKeyPressed = true; 
        }
        //Debug.Log(incVelocity);
        // If in the air (!grounded), and not moving (x,y both less than 0.2) -> Don't do anything
        if (!(incVelocity.x == 0 && incVelocity.y == 0 && !charController.isGrounded))
        {
            Vector3 velocity = new Vector3(incVelocity.x, inputVelocity.y, incVelocity.y);

            inputVelocity = velocity;
            
        }
    }

    void HandleMovementAnims()
    {
        if (charController.isGrounded)
        {
            var tempForward = Vector3.Dot(transform.forward, new Vector3(inputVelocity.x, 0.0f,inputVelocity.z)) * 10;
            var tempRight = Vector3.Dot(transform.right, new Vector3(inputVelocity.x, 0.0f,inputVelocity.z)) * 10;
            animator.SetFloat(forward, tempForward);
            animator.SetFloat(right, tempRight);
            
        }
    }

    void HandleRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 deltaVector = new Vector3(objPosition.x - mousePos.x, objPosition.y - mousePos.y, objPosition.z);

        float angle = Mathf.Atan2(deltaVector.y, deltaVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle - 90, 0));
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isJumpPresssed = true;
        }
        if (ctx.canceled)
        {
            isJumpPresssed = false;
        }
        if (ctx.performed && charController.isGrounded)
        {
            inputVelocity.y = jumpVelocity;
        }
    }


    void updateHorizontalAndLater()
    {
        

    }

    public void onFire(InputAction.CallbackContext ctx)
    {   
        if (ctx.started) {
            Debug.Log(animator.GetFloat(forward));
            Debug.Log(animator.GetFloat(right));

            //Debug.Log();
        }
    }
}
