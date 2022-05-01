using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayerMask;

    private MoveDirection moveDir;

    //public AudioSource woodSoundEffect;
    //public AudioSource grassWalkingSoundEffect;
    public float moveSpeed = 300f;
    public float runMoveSpeed = 500f;
    private bool isRunning;

    public bool inDialogue;

    private Animator animator;

    private PlayerInput playerInput;

    public float speakCooldown = 0.5f;
    public float speakCooldownLeft;

    private GameObject hornLamp;
    Vector2 originalPos;

    public static bool inLoadingZone;

    public GameObject interactionZones;

    public bool mirroredPlayer;

    public Vector3 direction;

    [HideInInspector]
    public Rigidbody2D rb2;

    [HideInInspector]
    public bool carryingObject, isFacingNPC;

    public GameObject carriedObject;

    //Variables for audio 
    private float stepRate = 0.5f;
    private float stepCoolDown;
    [SerializeField] private AudioClip clip;

    private enum MoveDirection
    {
        None, Up, Down, Left, Right
    }

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
        //woodSoundEffect = GetComponent<AudioSource>();
        //grassWalkingSoundEffect = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();
    }

    //public void OnCollisionEnter2D(Collision collision)
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
        //Talking to NPC cooldown
        if (speakCooldownLeft > 0)
        {
            speakCooldownLeft -= Time.deltaTime;
        }

        //Finding curently enabled interaction zone
        interactionZones = GameObject.FindGameObjectWithTag("interactionZone");

        CarryObjectToScene();

        PlayFootStepSFX();

        CheckIfMoving();

        //Check if running
        if (playerInput.actions["Dash"].IsPressed())
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        //Movement
        if (inDialogue == false)
        {
            //Get input;
            GetInput();

            //Set direction
            ChangeDirection();
        }
        else
        {
            direction = new Vector2(0, 0);
            moveDir = MoveDirection.None;
        }
    }

    private void FixedUpdate()
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

    private void PlayFootStepSFX()
    {
        stepCoolDown -= Time.deltaTime;

        if (stepCoolDown <= 0f && animator.GetBool("isMoving"))
        {
            stepCoolDown = stepRate;
            AudioManager.Instance.PlaySound(clip, 0.5f);
        }
    }

    private void CheckIfMoving()
    {
        if ((Mathf.Abs(rb2.velocity.x) > 0.01 || Mathf.Abs(rb2.velocity.y) > 0.01) && inLoadingZone == false)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// Checks for player input and sets direction to last pressed key
    /// </summary>
    private void GetInput()
    {
        if (playerInput.actions["Up"].triggered)
        {
            moveDir = MoveDirection.Up;
        }
        else if (playerInput.actions["Right"].triggered)
        {
            moveDir = MoveDirection.Right;
        }
        else if (playerInput.actions["Left"].triggered)
        {
            moveDir = MoveDirection.Left;
        }
        else if (playerInput.actions["Down"].triggered)
        {
            moveDir = MoveDirection.Down;
        }

        if (playerInput.actions["Up"].WasReleasedThisFrame())
        {
            moveDir = MoveDirection.None;
            CheckLastDirection();
        }
        else if (playerInput.actions["Down"].WasReleasedThisFrame())
        {
            moveDir = MoveDirection.None;
            CheckLastDirection();
        }
        else if (playerInput.actions["Left"].WasReleasedThisFrame())
        {
            moveDir = MoveDirection.None;
            CheckLastDirection();
        }
        else if (playerInput.actions["Right"].WasReleasedThisFrame())
        {
            moveDir = MoveDirection.None;
            CheckLastDirection();
        }
    }

    /// <summary>
    /// Checks for last pressed key in case player presses multiple keys at once
    /// </summary>
    private void CheckLastDirection()
    {
        if (playerInput.actions["Up"].IsPressed())
        {
            moveDir = MoveDirection.Up;
        }
        else if (playerInput.actions["Right"].IsPressed())
        {
            moveDir = MoveDirection.Right;
        }
        else if (playerInput.actions["Left"].IsPressed())
        {
            moveDir = MoveDirection.Left;
        }
        else if (playerInput.actions["Down"].IsPressed())
        {
            moveDir = MoveDirection.Down;
        }
    }

    /// <summary>
    /// Sets player movement and animations based on player direction
    /// </summary>
    private void ChangeDirection()
    {
        switch (moveDir)
        {
            case MoveDirection.Up:
                PlayerUp();
                break;
            case MoveDirection.Left:
                PlayerLeft();
                break;
            case MoveDirection.Down:
                PlayerDown();
                break;
            case MoveDirection.Right:
                PlayerRight();
                break;
            case MoveDirection.None:
                direction = new Vector2(0, 0);
                break;
        }
    }

    private void PlayerUp()
    {
        if (mirroredPlayer == false)
        {
            direction = new Vector2(0, 1);
            hornLamp.transform.position = new Vector2(transform.position.x - 0.56f, transform.position.y + 0.3f);
            if (animator.GetBool("isMoving"))
            {
                animator.SetTrigger("Up");
            }
            animator.SetBool("LastUp", true);
            animator.SetBool("LastDown", false);
            animator.SetBool("LastRight", false);
            animator.SetBool("LastLeft", false);
        }
        else
        {
            direction = new Vector2(0, -1);
            hornLamp.transform.localPosition = originalPos;
            if (animator.GetBool("isMoving"))
            {
                animator.SetTrigger("Down");
            }
            animator.SetBool("LastUp", false);
            animator.SetBool("LastDown", true);
            animator.SetBool("LastRight", false);
            animator.SetBool("LastLeft", false);
        }
    }

    private void PlayerDown()
    {
        if (mirroredPlayer == false)
        {
            direction = new Vector2(0, -1);
            hornLamp.transform.localPosition = originalPos;
            if (animator.GetBool("isMoving"))
            {
                animator.SetTrigger("Down");
            }
            animator.SetBool("LastUp", false);
            animator.SetBool("LastDown", true);
            animator.SetBool("LastRight", false);
            animator.SetBool("LastLeft", false);
        }
        else
        {
            direction = new Vector2(0, 1);
            hornLamp.transform.position = new Vector2(transform.position.x - 0.56f, transform.position.y + 0.3f);
            if (animator.GetBool("isMoving"))
            {
                animator.SetTrigger("Up");
            }
            animator.SetBool("LastUp", true);
            animator.SetBool("LastDown", false);
            animator.SetBool("LastRight", false);
            animator.SetBool("LastLeft", false);
        }
    }

    private void PlayerLeft()
    {
        direction = new Vector2(-1, 0);
        hornLamp.transform.localPosition = originalPos;
        if (animator.GetBool("isMoving"))
        {
            animator.SetTrigger("Left");
        }
        animator.SetBool("LastUp", false);
        animator.SetBool("LastDown", false);
        animator.SetBool("LastRight", false);
        animator.SetBool("LastLeft", true);
    }

    private void PlayerRight()
    {
        direction = new Vector2(1, 0);
        hornLamp.transform.localPosition = originalPos;
        if (animator.GetBool("isMoving"))
        {
            animator.SetTrigger("Right");
        }
        animator.SetBool("LastUp", false);
        animator.SetBool("LastDown", false);
        animator.SetBool("LastRight", true);
        animator.SetBool("LastLeft", false);
    }

    private void CarryObjectToScene()
    {
        //Carry object across scenes
        if (carryingObject == true && inLoadingZone == true)
        {
            interactionZones.GetComponent<pushPullObjects>();
            carriedObject = pushPullObjects.currentMovable;

            DontDestroyOnLoad(carriedObject);
        }
    }
}
