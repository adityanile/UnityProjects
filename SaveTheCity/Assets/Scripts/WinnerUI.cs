using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerUI : MonoBehaviour
{
    public LevelManager levelManager;
    public InGameUI gameUI;
    private PauseManager pauseManager;
    private GameMusic gameMusic;

    public GameObject mainmark;
    public GameObject maze1mark;
    public GameObject maze2mark;
    public GameObject maze3mark;

    public GameObject panel;
    public GameObject rightpanel;
    public GameObject leftpanel;
    public GameObject middlepanel;

    public ParticleSystem powerfulaura;

    private AudioSource playaudio;
    public AudioClip applause;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponent<LevelManager>();
        gameUI = GameObject.Find("UIManager").GetComponent<InGameUI>();
        pauseManager = GameObject.Find("PauseUI").GetComponent<PauseManager>();
        playaudio = GetComponent<AudioSource>();
        gameMusic = GameObject.Find("GameAudio").GetComponent<GameMusic>();

        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainMark"))
        {
            rightpanel.SetActive(false);
            panel.SetActive(true);
            mainmark.SetActive(true);
        }
        if (other.gameObject.CompareTag("Maze1Mark"))
        {
            rightpanel.SetActive(false);
            panel.SetActive(true);
            maze1mark.SetActive(true);
        }
        if (other.gameObject.CompareTag("Maze2Mark"))
        {
            rightpanel.SetActive(false);
            panel.SetActive(true);
            maze2mark.SetActive(true);
        }
        if (other.gameObject.CompareTag("Maze3Mark"))
        {
            rightpanel.SetActive(false);
            panel.SetActive(true);
            maze3mark.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainMark"))
        {
            rightpanel.SetActive(true);
            panel.SetActive(false);
            mainmark.SetActive(false);

            playaudio.PlayOneShot(applause, 1);
            powerfulaura.Play();       // Start Powerful Aura When Player Submits Crystals at winnerbase
        }
        if (other.gameObject.CompareTag("Maze1Mark"))
        {
            rightpanel.SetActive(true);
            panel.SetActive(false);
            maze1mark.SetActive(false);
        }
        if (other.gameObject.CompareTag("Maze2Mark"))
        {
            rightpanel.SetActive(true);
            panel.SetActive(false);
            maze2mark.SetActive(false);
        }
        if (other.gameObject.CompareTag("Maze3Mark"))
        {
            rightpanel.SetActive(true);
            panel.SetActive(false);
            maze3mark.SetActive(false);
        }
    }

    public void OnClickMaze1()
    {
        transform.position = new Vector3(-77, transform.position.y, 47); // Teleport to Start of Maze1
        
        // GamePlay Settings
        levelManager.maze1completed = false;
        gameUI.maintime.minutes = 0;
        gameUI.maintime.seconds = 0;
        levelManager.currentmaze = 1;
        levelManager.alreadyattheend = false;

        // Pause UI Powerup status Update
        pauseManager.playingagain = true;

        gameMusic.Maze1Audio();

    }
    public void OnClickMaze2()
    {
        transform.position = new Vector3(-657, transform.position.y, -1007); // Teleport to Start of Maze1

        // GamePlay Settings
        levelManager.maze2completed = false;
        gameUI.maintime.minutes = 0;
        gameUI.maintime.seconds = 0;
        levelManager.currentmaze = 2;
        levelManager.alreadyattheend = false;

        gameMusic.Maze2Audio();

    }
    public void OnClickMaze3()
    {
        transform.position = new Vector3(-511, transform.position.y, 407); // Teleport to Start of Maze1

        // GamePlay Settings
        levelManager.maze3completed = false;
        gameUI.maintime.minutes = 0;
        gameUI.maintime.seconds = 0;
        levelManager.currentmaze = 3;
        levelManager.alreadyattheend = false;

        gameMusic.Maze3Audio();

    }
}
