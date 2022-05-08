using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static List<Quest> quests = new List<Quest>();
    public static List<Item> inventory = new List<Item>();

    // Holds names of npc who have already used forced interaction 
    public static List<string> forcedDialogueEncounters = new List<string>();

    // Used to set player position when entering new scene
    public static Vector2 spawnPoint;

    private static bool lampOn;
    [HideInInspector]
    public bool callPostQuest;

    [HideInInspector]
    public GameObject item;
    public PlayerInput playerInput;
    public TrustMeter trustMeter;
    public QuestUI questUI;
    public GameObject spotShadow;

    //private SpriteLibrary spriteLibrary;
    public Light2D hornLamp;
    public float maxBrightness = 0.5f;

    private Material mat;
    public Renderer render;
    private static float lightIntensity = 0;
    private static float hornIntensity = 0;
    private static float lightChangeRate = 0.5f;
    private static float hornChangeRate = 1f;


    private void Awake()
    {
        //spriteLibrary = gameObject.GetComponent<SpriteLibrary>();
        mat = gameObject.GetComponent<Renderer>().sharedMaterial;
        //Debug.Log("Vector: " + spawnPoint);
        gameObject.transform.position = spawnPoint;
    }

    private void Start()
    {
        questUI.UpdateList();
    }

    void Update()
    {
        ToggleLitSprite();
        ToggleLampLight();
    }

    #region lights/shadow
    private void ToggleLitSprite()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "MidQReview":
                LitScene();
                break;
            case "MonsterTownThing":
                DarkScene();
                break;
            case "mazeScene":
                DarkScene();
                break;
            case "Vampires Shop":
                DarkScene();
                break;
            case "MonsterCave":
                DarkScene();
                break;
            case "ActualPuzzleScene":
                DarkScene();
                break;
            case "ForestScene":
                LitScene();
                break;
            case "NEWMAZE":
                DarkScene();
                break;
            case "MirrorScene":
                DarkScene();
                break;
            case "Witch house":
                Indoors();
                break;
        }
        //spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>("SpriteLibrary/Glow");
    }

    private void DarkScene()
    {
        lampOn = true;
        spotShadow.SetActive(true);
        spotShadow.transform.position = new Vector3(transform.position.x + 0.054f, transform.position.y - 1.75f, transform.position.z);
    }

    private void Indoors()
    {
        lampOn = false;
        spotShadow.SetActive(true);
        spotShadow.transform.position = new Vector3(transform.position.x + 0.054f, transform.position.y - 1.75f, transform.position.z);
    }

    private void LitScene()
    {
        lampOn = false;
        spotShadow.SetActive(true);
        spotShadow.transform.position = new Vector3(transform.position.x - 0.2700005f, transform.position.y - 1.75f, transform.position.z);
    }

    private void ToggleLampLight()
    {
        if (lampOn)
        {
            lightIntensity += lightChangeRate * Time.deltaTime;
            hornIntensity += hornChangeRate * Time.deltaTime;
        }
        else
        {
            lightIntensity -= lightChangeRate * Time.deltaTime;
            hornIntensity -= hornChangeRate * Time.deltaTime;
        }

        lightIntensity = Mathf.Clamp(lightIntensity, 0, maxBrightness);
        hornIntensity = Mathf.Clamp(lightIntensity, 0, 2);

        hornLamp.intensity = lightIntensity;

        mat.SetColor("Color_18748F50", Color.yellow * hornIntensity);
    }
    #endregion


    #region quests
    public void GetQuest(QuestGiver npc)
    {
        npc.AcceptQuest();
    }

    /// <summary>
    /// Deactivate quest and sets CallPostQuest to true after interaction
    /// </summary>
    /// <param name="npc"></param>
    public void ProgressQuest(QuestGiver npc)
    {
        if (npc.quest.isActive)
        {
            foreach (Item item in inventory)
            {
                if (npc.quest.goal.IsReached(item) && npc.quest.isComplete == false)
                {
                    //Debug.Log("calling post quest. Item: " + item.itemName);
                    callPostQuest = true;
                    npc.quest.isComplete = true;
                    npc.quest.isActive = false;
                    return;
                }
            }
            return;
        }
    }

    #endregion
}
