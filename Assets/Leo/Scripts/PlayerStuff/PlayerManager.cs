using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    private bool lampOn;

    [HideInInspector]
    public GameObject item;
    [HideInInspector]
    public TrustMeter trustMeter;

    private SpriteLibrary spriteLibrary;
    private Light2D hornLamp;

    private static float intensity = 0;
    private static float changeRate = 0.5f;

    private void Awake()
    {
        spriteLibrary = gameObject.GetComponent<SpriteLibrary>();
        trustMeter = GameObject.Find("Slider").GetComponent<TrustMeter>();
        hornLamp = GameObject.Find("HornLampLight").GetComponent<Light2D>();
    }

    void Update()
    {
        PickUpItem();
        ToggleLitSprite();
        ToggleLampLight();
    }

    private void ToggleLitSprite()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (lampOn)
            {
                spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>("SpriteLibrary/Regular");
                lampOn = false;
            } else
            {
                spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>("SpriteLibrary/Glow");
                lampOn = true;
            }
            
        }
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

        intensity = Mathf.Clamp(intensity, 0, 1);

        hornLamp.intensity = intensity;
    }

    public void GetQuest(QuestGiver npc)
    {   
        if (npc.quest.isAvailable)
        {
            if (!npc.quest.isActive)
            {
                npc.AcceptQuest();
            }
        }
    }

    public void ProgressQuest(QuestGiver npc)
    {
        if (npc.quest.isActive)
        {
            foreach (Item item in inventory)
            {
                if (npc.quest.goal.IsReached(item))
                {
                    trustMeter.StartCoroutine("AddProgress", 0.5f);
                    npc.quest.isComplete = true;
                    npc.quest.isActive = false;
                    return;
                }
            }
            return;
        }
    }

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
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Picked up item");
                item.GetComponent<ItemInteractable>().pickUp();
            }
        }
    }

}
