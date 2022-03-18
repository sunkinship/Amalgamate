using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask dashLayerMask;

    private float moveSpeed = 3f;
    private float dashSpeed;

    public Animator animator;

    Vector2 movement;

    public bool constantSprint;

    public PlayerInput playerInput;

    private float dashCooldown = 1.5f;
    public float currentDashCooldown;


    private enum State
    {
        Normal,
        Dashing,
    }

    public Rigidbody2D rb2;

    public string lastFacingDirection = "RIGHT";

    private Vector3 lastMoveDirection;

    private Vector3 direction;

    private Vector3 dashDir;

    private State state;


    private void Awake()
    {
        state = State.Normal;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        /*movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);*/



        GetInput();
        currentDashCooldown = currentDashCooldown - Time.deltaTime;

    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        switch(state)
        {
            case State.Normal:
                rb2.velocity = direction * moveSpeed;

                //if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["DASH"]) && currentDashCooldown < 0)
                if (playerInput.actions["Dash"].IsPressed() && currentDashCooldown < 0)
                {
                    dashDir = direction;

                    float dashDistance = 5f;

                    Vector3 dashPosition = transform.position + direction * dashDistance;
                    RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, direction, dashDistance, dashLayerMask);
                    if (raycastHit2d.collider != null)
                    {
                        dashPosition = raycastHit2d.point;
                    }

                    rb2.MovePosition(dashPosition);

                }
                break;
            case State.Dashing:
                rb2.velocity = dashDir.normalized * dashSpeed;
                break;
        }

    }

    public void GetInput()
    {
        

        switch (state)
        {
            case State.Normal:

                //Vector2 input = playerInput.actions["Movement"].ReadValue<Vector2>();

                float moveX = 0f;
                float moveY = 0f;


                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["SPRINT"])) moveSpeed = 5; else moveSpeed = 3;
                if (playerInput.actions["Sprint"].IsPressed() || constantSprint == true) moveSpeed = 5; else moveSpeed = 3;
                if (playerInput.actions["Sprint"].IsPressed() && constantSprint == true) moveSpeed = 3;

                direction = Vector2.zero;

                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
                if (playerInput.actions["Up"].IsPressed())
                {
                    if (playerInput.actions["Left"].IsPressed() == false && playerInput.actions["Down"].IsPressed() == false && playerInput.actions["Right"].IsPressed() == false)
                    {
                        animator.SetTrigger("Up");
                    }
                    moveY = +1f;
                    lastFacingDirection = "UP";
                }
                //else animator.ResetTrigger("Up");


                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
                if (playerInput.actions["Down"].IsPressed())
                {
                    moveY = -1f;
                    lastFacingDirection = "DOWN";
                    if (playerInput.actions["Left"].IsPressed() == false && playerInput.actions["Right"].IsPressed() == false)
                    {
                        animator.SetTrigger("Down");
                    }

                }
                //else animator.ResetTrigger("Down");

                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
                if (playerInput.actions["Left"].IsPressed())
                {
                    moveX = -1f;
                    lastFacingDirection = "LEFT";
                    if (playerInput.actions["Right"].IsPressed() == false)
                    {
                        animator.SetTrigger("Left");
                    }

                }
                //else animator.ResetTrigger("Left");


                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
                if (playerInput.actions["Right"].IsPressed())
                {
                    moveX = +1f;
                    lastFacingDirection = "RIGHT";
                    animator.SetTrigger("Right");

                }
                else animator.ResetTrigger("Right");

                direction = new Vector3(moveX, moveY).normalized;
                if(moveX != 0 || moveY != 0)
                {
                    lastMoveDirection = direction;
                }

                //if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["DASH"]) && currentDashCooldown < 0)
                if (playerInput.actions["Dash"].IsPressed() && currentDashCooldown < 0)
                {
                    dashDir = lastMoveDirection;
                    dashSpeed = 25f;
                    state = State.Dashing;
                }

                break;
            case State.Dashing:
                float dashSpeedDropMultiplier = 4f;
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                float dashSpeedMinimum = 3f;
                if(dashSpeed < dashSpeedMinimum)
                {
                    currentDashCooldown = dashCooldown;
                    state = State.Normal;
                }
                break;

        }

    }

    //private bool CanMove(Vector3 dir, float distance)
    //{
    //    return Physics2D.Raycast(transform.position, dir, distance * Time.deltaTime).collider == null;
    //}

    //private void handleDash()
    //{
    //    if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["DASH"]))
    //    {
    //        dashDir = direction;

    ////        INSTANT TELE -DASH
    //        float dashDistance = 5f;

    //        Vector3 dashPosition = transform.position + direction * dashDistance;
    //        RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, lastMoveDirection, dashDistance, dashLayerMask);
    //        if (raycastHit2d.collider != null)
    //        {
    //            dashPosition = raycastHit2d.point;
    //        }

    //        rb2.MovePosition(dashPosition);
    //    }
    //}

}
