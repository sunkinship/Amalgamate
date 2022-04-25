using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask dashLayerMask;
    [SerializeField] private LayerMask wallLayerMask;

    public AudioSource woodSoundEffect;
    public AudioSource grassWalkingSoundEffect;
    public float moveSpeed = 6f;
    private float dashSpeed;

    public bool goToMazeExit;

    public bool inDialogue;

    private Animator animator;

    Vector2 movement;

    private PlayerInput playerInput;

    private float dashCooldown = 1.5f;
    public float currentDashCooldown;

    public float speakCooldown = .5f;
    public float speakCooldownLeft;

    private GameObject hornLamp;
    Vector2 originalPos;

    public static bool inLoadingZone;

    public GameObject interactionZones;

    private enum State
    {
        Normal,
        Dashing,
    }

    public string lastFacingDirection = "RIGHT";

    private Vector3 lastMoveDirection;

    public Vector3 direction;

    public Vector3 rayDirection;

    private Vector3 dashDir;

    private State state;

    [HideInInspector]
    public Rigidbody2D rb2;

    [HideInInspector]
    public bool carryingObject, isFacingNPC;

    public GameObject carriedObject;

    //Variables for audio 
    private float stepRate = 0.5f;
    private float stepCoolDown;
    [SerializeField] private AudioClip clip;



    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        hornLamp = GameObject.Find("HornLampLight");
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        state = State.Normal;
        originalPos = new Vector2(hornLamp.transform.localPosition.x, hornLamp.transform.localPosition.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        woodSoundEffect = GetComponent<AudioSource>();
        grassWalkingSoundEffect = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame

    public void OnCollis(Collider other)
    {

        Debug.Log("Help");
        if (other.tag == "Wood")
        {
            Debug.Log("Sound");
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                woodSoundEffect.UnPause();
            }
            else
            {
                woodSoundEffect.Pause();
            }
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{

    //    Debug.Log("Help");
    //    if (tag == "Wood")
    //    {
    //        Debug.Log("Sound");
    //        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    //        {
    //            woodSoundEffect.UnPause();
    //        }
    //        else
    //        {
    //            woodSoundEffect.Pause();
    //        }
    //    }
    //}
    void Update()
    {

        interactionZones = GameObject.FindGameObjectWithTag("interactionZone");

        if (carryingObject == true && inLoadingZone == true)
        {
            interactionZones.GetComponent<pushPullObjects>();
            carriedObject = pushPullObjects.currentMovable;

            DontDestroyOnLoad(carriedObject);
        }

        //Play footstep sfx
        stepCoolDown -= Time.deltaTime;

        if (stepCoolDown <= 0f && animator.GetBool("isMoving"))
        {
            stepCoolDown = stepRate;
            AudioManager.Instance.PlaySound(clip);
        }


        /*movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);*/

        //SetRayDirection();

        //if (pushPullObjects.isMovingObject)
        //{
        //    RaycastHit2D preventDorppingObject = Physics2D.Raycast(transform.position, rayDirection, 0.5f, wallLayerMask);
        //    Debug.DrawLine(transform.position, rayDirection, Color.red);

        //    if (preventDorppingObject == true)
        //    {
        //        pushPullObjects.canDropObject = false;
        //        Debug.Log("ray hit: " + rayDirection);
        //    }
        //    else
        //    {
        //        pushPullObjects.canDropObject = true;
        //        Debug.Log("ray not hit: " + rayDirection);
        //    }
        //}

        RaycastHit2D stopAniTransition = Physics2D.Raycast(transform.position, rayDirection, 10f, wallLayerMask);
        //Debug.Log("shooting raycast dir: " + direction);

        if (stopAniTransition == true)
        {
            //Debug.Log("aniStop ray hit");
            animator.SetBool("isMoving", false);
        }
        else
        {
            //Debug.Log("aniStop ray not hit");
            animator.SetBool("isMoving", true);
        }

        if ((Mathf.Abs(rb2.velocity.x) > 0.01 || Mathf.Abs(rb2.velocity.y) > 0.01) && inLoadingZone == false)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (inDialogue == false)
        {
            GetInput();
        }

        speakCooldownLeft = speakCooldownLeft - Time.deltaTime;


        //if(goToMazeExit == true)
        //{
        //    this.gameObject.transform.position = GameObject.Find("exitMazeLocation").transform.position;
        //    goToMazeExit = false;
        //}

    }

    void FixedUpdate()
    {
        if (inDialogue == false)
        {
            Move();
        }

    }


    public void Move()
    {
        switch (state)
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

                direction = Vector2.zero;

                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
                if (playerInput.actions["Up"].IsPressed() && inLoadingZone == false)
                {
                    hornLamp.transform.position = new Vector2(transform.position.x - 0.56f, transform.position.y + 0.3f);
                    if (playerInput.actions["Left"].IsPressed() == false && playerInput.actions["Down"].IsPressed() == false && playerInput.actions["Right"].IsPressed() == false)
                    {
                        //Debug.Log("set trigger up");
                        animator.SetTrigger("Up");
                    }
                    moveY = +1f;
                    lastFacingDirection = "UP";
                    animator.SetBool("LastUp", true);
                    animator.SetBool("LastDown", false);
                    animator.SetBool("LastRight", false);
                    animator.SetBool("LastLeft", false);
                }
                //else animator.ResetTrigger("Up");


                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
                if (playerInput.actions["Down"].IsPressed() && inLoadingZone == false)
                {
                    hornLamp.transform.localPosition = originalPos;
                    moveY = -1f;
                    lastFacingDirection = "DOWN";
                    animator.SetBool("LastUp", false);
                    animator.SetBool("LastDown", true);
                    animator.SetBool("LastRight", false);
                    animator.SetBool("LastLeft", false);
                    if (playerInput.actions["Left"].IsPressed() == false && playerInput.actions["Right"].IsPressed() == false)
                    {
                        //Debug.Log("set trigger down");
                        animator.SetTrigger("Down");
                    }
                }
                //else animator.ResetTrigger("Down");

                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
                if (playerInput.actions["Left"].IsPressed() && inLoadingZone == false)
                {
                    hornLamp.transform.localPosition = originalPos;
                    moveX = -1f;
                    lastFacingDirection = "LEFT";
                    animator.SetBool("LastUp", false);
                    animator.SetBool("LastDown", false);
                    animator.SetBool("LastRight", false);
                    animator.SetBool("LastLeft", true);
                    if (playerInput.actions["Right"].IsPressed() == false)
                    {
                        //Debug.Log("set trigger left");
                        animator.SetTrigger("Left");
                    }
                }
                //else animator.ResetTrigger("Left");

                //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
                if (playerInput.actions["Right"].IsPressed() && inLoadingZone == false)
                {
                    hornLamp.transform.localPosition = originalPos;
                    animator.SetTrigger("Right");
                    moveX = +1f;
                    lastFacingDirection = "RIGHT";
                    animator.SetBool("LastUp", false);
                    animator.SetBool("LastDown", false);
                    animator.SetBool("LastRight", true);
                    animator.SetBool("LastLeft", false);
                    //Debug.Log("set trigger right");
                }
                //else animator.ResetTrigger("Right");

                direction = new Vector3(moveX, moveY).normalized;
                //rayDirection = new Vector3(moveX, moveY).normalized;

                if (moveX != 0 || moveY != 0)
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
                if (dashSpeed < dashSpeedMinimum)
                {
                    currentDashCooldown = dashCooldown;
                    state = State.Normal;
                }
                break;

        }

    }

    private void SetRayDirection()
    {
        switch (lastFacingDirection)
        {
            case "UP":
                rayDirection = new Vector3(0, 1).normalized;
                break;
            case "DOWN":
                rayDirection = new Vector3(0, -1).normalized;
                break;
            case "LEFT":
                rayDirection = new Vector3(-1, 0).normalized;
                break;
            case "RIGHT":
                rayDirection = new Vector3(1, 0).normalized;
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
