using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager MusicInstance;
    public AudioSource BGM;
    private int sceneIndex;
    private string currentClip = " ";

    [HideInInspector]
    public AudioClip mainMenu, credits, gameScene, boss, win, intro, death;

    public void Awake()
    {
        if (MusicInstance != null && MusicInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        MusicInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        mainMenu = Resources.Load<AudioClip>("Purple_Planet");
        credits = Resources.Load<AudioClip>("Tiny_Blocks");
        gameScene = Resources.Load<AudioClip>("InfiniteDoors");
        boss = Resources.Load<AudioClip>("Stupid_Dancer");
        win = Resources.Load<AudioClip>("Feelin' Good");
        intro = Resources.Load<AudioClip>("FutureWorld");
        death = Resources.Load<AudioClip>("Lament");

        BGM = GetComponent<AudioSource>();
    }

    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (sceneIndex)
        {
            case 0:
                currentClip = "Purple_Planet";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = mainMenu;
                    BGM.Play();
                }
                break;
            case 1:
                currentClip = "Tiny_Blocks";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = credits;
                    BGM.Play();
                }
                break;
            case 2:
                currentClip = "FutureWorld";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = intro;
                    BGM.Play();
                }
                break;
            case 3:
                currentClip = "Lament";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = death;
                    BGM.Play();
                }
                break;
            case 4:
                currentClip = "InfiniteDoors";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = gameScene;
                    BGM.Play();
                }
                break;
            case 5:
                currentClip = "InfiniteDoors";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = gameScene;
                    BGM.Play();
                }
                break;
            case 6:
                currentClip = "Stupid_Dancer";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = boss;
                    BGM.Play();
                }
                break;
            case 7:
                currentClip = "Feelin' Good";
                if (BGM.clip.name != currentClip)
                {
                    BGM.Stop();
                    BGM.clip = win;
                    BGM.Play();
                }
                break;
        }
    }
}
