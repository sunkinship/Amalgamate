using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    private bool lampOn;
    [HideInInspector]
    public bool callPostQuest;

    [HideInInspector]
    public GameObject item;
    public PlayerInput playerInput;
    public TrustMeter trustMeter;

    //private SpriteLibrary spriteLibrary;
    public Light2D hornLamp;
    public float maxBrightness = 0.5f;

    private static float intensity = 0;
    private static float changeRate = 0.5f;

    private void Awake()
    {
        //spriteLibrary = gameObject.GetComponent<SpriteLibrary>();
    }

    void Update()
    {
        PickUpItem();
        ToggleLitSprite();
        ToggleLampLight();
    }

    #region lights
    private void ToggleLitSprite()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "MidQReview":
                lampOn = false;
                break;
            case "MonsterTownThing":
                lampOn = true;
                break;
            case "mazeScene":
                lampOn = true;
                break;
            case "Vampires Shop":
                lampOn = false;
                break;
            case "TestingScene":
                lampOn = true;
                break;
        }
        //spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>("SpriteLibrary/Glow");
    }

    private void ToggleLampLight()
    {
        if (lampOn)
        {
            intensity += changeRate * Time.deltaTime;
        }
        else
        {
            intensity -= changeRate * Time.deltaTime;
        }

        intensity = Mathf.Clamp(intensity, 0, maxBrightness);

        hornLamp.intensity = intensity;
    }
    #endregion


    #region quests
    public void GetQuest(QuestGiver npc)
    {   
        if (npc.quest.hasQuest)
        {
            if (!npc.quest.isActive)
            {
                npc.AcceptQuest();
            }
        }
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


    #region items
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            //Debug.Log("in range");
            item = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            //Debug.Log("left range");
            item = null;
        }
    }

    public void PickUpItem()
    {
        if (item != null)
        {
            if (playerInput.actions["Interact"].triggered)
            {
                //Debug.Log("Picked up item");
                item.GetComponent<ItemInteractable>().PickUp();
            }
        }
    }
    #endregion
}
