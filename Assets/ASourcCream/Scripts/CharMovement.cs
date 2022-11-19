using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharMovement : MonoBehaviour
{
    // <------------------------------- SETTINGS ------------------------------- //
    [SerializeField]
    private bool lookAtMouse = true;

    // <------------------------------- ATTACK ------------------------------- //
    private bool isAttacking;
    [SerializeField]
    private float inAttackSlowAmount = 2.0f;

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
    [SerializeField]
    private GameObject weaponR;

    private bool movementKeyPressed = false;

    private float currentRotation;
    [SerializeField]
    private float rotationSpeed = 10.0f;



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


    private float nextActionTime = 0.0f;
    private float period = 0.5f;


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
        HandleAttackAction();
        HandleMovementAnims();
        HandleGravity();
        HandleMovement();
    }

    public void HandleAttackAction() {
        if (Time.time > nextActionTime) { //TODO: FEEL FREE TO UNDO THIS DUMB SHIT also remove nextActionTime += period; in OnFire
            nextActionTime += period;
            if (isAttacking) {
            isAttacking = animator.GetCurrentAnimatorStateInfo(1).IsName("attack");
            }
        } 
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
        Debug.Log(isAttacking);
        if (isAttacking) {
            var movementSpeedAfterSlow = movementSpeed - inAttackSlowAmount;
            lateralVelocity *= movementSpeedAfterSlow;
        } else {
            lateralVelocity *= movementSpeed;
            var lookAt = new Vector3(lateralVelocity.x, 0.0f, lateralVelocity.y);
            if (lookAt.magnitude > 0) {
                transform.rotation = Quaternion.LookRotation(new Vector3(lateralVelocity.x, 0.0f, lateralVelocity.y)); 
            }
        }
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
            var multiplier = isAttacking ? 3 : 10;
            var tempForward = Vector3.Dot(transform.forward, new Vector3(inputVelocity.x, 0.0f,inputVelocity.z)) * multiplier;
            var tempRight = Vector3.Dot(transform.right, new Vector3(inputVelocity.x, 0.0f,inputVelocity.z)) * multiplier;
            animator.SetFloat(forward, tempForward);
            animator.SetFloat(right, tempRight);
        }
    }

    void HandleRotation()
    {
        if (lookAtMouse) {
            Vector3 mousePosition;
            Vector3 objPosition;
            Transform target = transform;
            float angle;
            mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            objPosition = Camera.main.WorldToScreenPoint(target.position);
            mousePosition.x = mousePosition.x - objPosition.x;
            mousePosition.y = mousePosition.y - objPosition.y;
            angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            var roatation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -angle + 90, 0)), Time.deltaTime * rotationSpeed);
            target.rotation = roatation;
        } else {

        }
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
    public void lookTowardCursor() {
        Vector3 mousePosition;
        Vector3 objPosition;
        Transform target = transform;
        float angle;
        mousePosition = Input.mousePosition;
        mousePosition.z = 10.0f;
        objPosition = Camera.main.WorldToScreenPoint(target.position);
        mousePosition.x = mousePosition.x - objPosition.x;
        mousePosition.y = mousePosition.y - objPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        var roatation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));
        target.rotation = roatation;
    }

    public void onFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started) {
            if (!isAttacking) {
                isAttacking = true;
                nextActionTime += period;
                animator.SetTrigger("Attack");
                lookTowardCursor();
            }
        }
    }

    public void onColide(Collider colider) {
        Debug.Log(colider.transform.gameObject.layer);
    }
}
