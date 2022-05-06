using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayerMask;

    public AudioSource woodSoundEffect;
    public AudioSource grassWalkingSoundEffect;

    [SerializeField]
    private float moveSpeed, runMoveSpeed, carryWalkSpeed, carryRunSpeed;

    public static bool isRunning, inDialogue, inLoadingZone;

    private Animator animator;

    private PlayerInput playerInput;

    public static float speakCooldown = .5f;
    public static float speakCooldownLeft;

    private GameObject hornLamp;
    Vector2 originalPos;

    public GameObject interactionZones;

    public bool mirroredPlayer;

    public string lastFacingDirection = "RIGHT";

    //private Vector3 lastMoveDirection;

    public Vector3 direction;

    [HideInInspector]
    public Rigidbody2D rb2;

    [HideInInspector]
    public static bool carryingObject, isFacingNPC;

    public GameObject carriedObject;

    //Variables for audio 
    private static float stepRate = 0.5f;
    private static float stepCoolDown;
    [SerializeField] private AudioClip clip;


    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        hornLamp = GameObject.Find("HornLampLight");
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        originalPos = new Vector2(hornLamp.transform.localPosition.x, hornLamp.transform.localPosition.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        woodSoundEffect = GetComponent<AudioSource>();
        grassWalkingSoundEffect = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        //iteractionZones = GameObject.FindGameObjectWithTag("interactionZone");

        //if (carryingObject == true && inLoadingZone == true)
        //{
        //    interactionZones.GetComponent<pushPullObjects>();
        //    carriedObject = pushPullObjects.currentMovable;

        //    DontDestroyOnLoad(carriedObject);
        //}


        //Check if running
        if (playerInput.actions["Dash"].IsPressed())
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Play footstep sfx
        stepCoolDown -= Time.deltaTime;

        if (stepCoolDown <= 0f && animator.GetBool("isMoving"))
        {
            stepCoolDown = stepRate;
            AudioManager.Instance.PlaySound(clip, 0.5f);
        }

        if ((Mathf.Abs(rb2.velocity.x) > 0.01 || Mathf.Abs(rb2.velocity.y) > 0.01) && inLoadingZone == false)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (inDialogue == false && inLoadingZone == false)
        {
            GetInput();
        }

        // Cool down for npc talking 
        speakCooldownLeft = speakCooldownLeft - Time.deltaTime;
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
        if (carryingObject)
        {
            if (isRunning == false)
            {
                rb2.velocity = direction * carryWalkSpeed * Time.deltaTime;
            }
            else
            {
                rb2.velocity = direction * carryRunSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (isRunning == false)
            {
                rb2.velocity = direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                rb2.velocity = direction * runMoveSpeed * Time.deltaTime;
            }
        }
    }

    public void GetInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        direction = Vector2.zero;

        //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
        if (playerInput.actions["Up"].IsPressed() && inLoadingZone == false && mirroredPlayer == false)
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
        if (playerInput.actions["Down"].IsPressed() && inLoadingZone == false && mirroredPlayer == false)
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
        #region mirrorInput
        if (playerInput.actions["Up"].IsPressed() && inLoadingZone == false && mirroredPlayer == true)
        {
            hornLamp.transform.position = new Vector2(transform.position.x - 0.56f, transform.position.y + 0.3f);
            if (playerInput.actions["Left"].IsPressed() == false && playerInput.actions["Down"].IsPressed() == false && playerInput.actions["Right"].IsPressed() == false)
            {
                //Debug.Log("set trigger up");
                animator.SetTrigger("Down");
            }
            moveY = -1f;
            lastFacingDirection = "DOWN";
            animator.SetBool("LastUp", false);
            animator.SetBool("LastDown", true);
            animator.SetBool("LastRight", false);
            animator.SetBool("LastLeft", false);
        }
        //else animator.ResetTrigger("Up");


        //if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
        if (playerInput.actions["Down"].IsPressed() && inLoadingZone == false && mirroredPlayer == true)
        {
            hornLamp.transform.localPosition = originalPos;
            moveY = +1f;
            lastFacingDirection = "UP";
            animator.SetBool("LastUp", true);
            animator.SetBool("LastDown", false);
            animator.SetBool("LastRight", false);
            animator.SetBool("LastLeft", false);
            if (playerInput.actions["Left"].IsPressed() == false && playerInput.actions["Right"].IsPressed() == false)
            {
                //Debug.Log("set trigger down");
                animator.SetTrigger("Up");
            }
        }
        #endregion mirrorInput
        //else animator.ResetTrigger("Right");

        direction = new Vector3(moveX, moveY).normalized;
        //rayDirection = new Vector3(moveX, moveY).normalized;

        //if (moveX != 0 || moveY != 0)
        //{
        //    lastMoveDirection = direction;
        //}
    }
}
