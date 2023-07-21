using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdditionalPowerUps : MonoBehaviour
{
    /*
     This is for 2 AdditionalPowerUp
     1. See Through Maze
     2. Show the correct path

    1.    If See Through is being used Player will not able to move from the current position
          See Through ability will be activated by clicking O key on the Keyboard.
          Maze will Disappear for 7 sec

    2.    Correct path is Highlighted on the ground
          Player can move along that path for 10 sec
          Can be activated By P key
          

    */

    // UI Management of AdditionalPowerrUP 
    public GameObject middlepanel;
    public GameObject addtionalpowerup;    // Notice 1
    public GameObject addtionalpowerup2;   // Notice 2
    public TextMeshProUGUI additionalpower1update;
    public TextMeshProUGUI additionalpower2update;


    private GameObject originalmaze2;
    private GameObject originalmaze1;
    private GameObject originalmaze3;

    private LevelManager levelmanager;

    public int waittime = 7;

    private PlayerController playerController;
    private bool seethroughtaken = false;

    private GameObject maze2path;
    private GameObject maze1path;
    private GameObject maze3path;

    [SerializeField] private bool showpath = false;
    private int pathtime = 10;

    // When to Activate AdditionalPowerUps
    private InGameUI gameUI;

    int maze1activatetime = 1;  //in minutes
    int maze2activatetime = 1;
    int maze3activatetime = 1;

    public bool startpowerup1 = false;
    public bool startpowerup2 = false;

    public bool onceamaze = true;

    // How much PowerUp To Give according to maze
    public int maze1power1count = 5;
    public int maze1power2count = 5;
    public int maze2power1count = 5;
    public int maze2power2count = 5;
    public int maze3power1count = 5;
    public int maze3power2count = 5;


    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("UIManager").GetComponent<InGameUI>();

        maze1path.SetActive(false);
        maze2path.SetActive(false);
        maze3path.SetActive(false);

        addtionalpowerup.SetActive(false);
        addtionalpowerup2.SetActive(false);
        additionalpower1update.text = "";
        additionalpower2update.text = "";
    }

    void Awake()
    {
        // Initialise Refereences here bacause it starts before start

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        levelmanager = GameObject.Find("Player").GetComponent<LevelManager>();

        maze1path = GameObject.Find("PathMaze1");
        maze2path = GameObject.Find("PathMaze2");
        maze3path = GameObject.Find("PathMaze3");

        originalmaze2 = GameObject.Find("OriginalMaze2");
        originalmaze1 = GameObject.Find("OriginalMaze1");
        originalmaze3 = GameObject.Find("OriginalMaze3");

        middlepanel.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (!startpowerup1 && !startpowerup2 && onceamaze) {
            WhenToStartPowerUp();    // Get when to Start the power up
        }

        if (startpowerup1)      
        {
            StartPowerUp1();
        }
        if (startpowerup2)
        {
            StartPowerUp2();
        }

        if (startpowerup1 || startpowerup2) {
            WhenToStopPowerUp();  // Get when to Stop the power up
        }
    }

    void StartPowerUp1()
    {
        if (Input.GetKeyDown(KeyCode.O) && !seethroughtaken)
        {
            addtionalpowerup2.SetActive(false);  // UI Notice

            seethroughtaken = true;
            playerController.motionrestricted = true;

            if (levelmanager.currentmaze == 1)
            {
                maze1power1count--;
                additionalpower1update.text = "See Through (Reamining) :- " + maze1power1count + " / 5 ";
                StartCoroutine(ShowMaze());
                originalmaze2.SetActive(false);
            }
            else if (levelmanager.currentmaze == 2)
            {
                maze2power1count--;
                additionalpower1update.text = "See Through (Reamining) :- " + maze2power1count + " / 5 ";
                StartCoroutine(ShowMaze());
                originalmaze3.SetActive(false);
            }
            else if (levelmanager.currentmaze == 3)
            {
                maze3power1count--;
                additionalpower1update.text = "See Through (Reamining) :- " + maze3power1count + " / 5 ";
                StartCoroutine(ShowMaze());
                originalmaze1.SetActive(false);
            }
        }
    }
    void StartPowerUp2()
    {
        if (Input.GetKeyDown(KeyCode.P) && !showpath)
        {
            showpath = true;

            addtionalpowerup2.SetActive(false);  // UI Notice

            if (levelmanager.currentmaze == 1)
            {
                StartCoroutine(ClosePath());
                maze1path.SetActive(true);
                maze1power2count--;
                additionalpower2update.text = "Path Shower (Reamining) :- " + maze1power2count + " / 5 ";
            }
            else if (levelmanager.currentmaze == 2)
            {
                StartCoroutine(ClosePath());
                maze2path.SetActive(true);
                maze2power2count--;
                additionalpower2update.text = "Path Shower (Reamining) :- " + maze2power2count + " / 5 ";
            }
            else if (levelmanager.currentmaze == 3)
            {
                StartCoroutine(ClosePath());
                maze3path.SetActive(true);
                maze3power2count--;
                additionalpower2update.text = "Path Shower (Reamining) :- " + maze3power2count + " / 5 ";
            }
        }
    }

    void WhenToStartPowerUp()
    {
        // When to Start powerUp in maze 1
        if(gameUI.maintime.minutes == maze1activatetime && levelmanager.currentmaze == 1 && !levelmanager.maze1completed)
        {
            startpowerup1 = true;
            startpowerup2 = true;
            Debug.Log("Additional Power Up Activated");

            middlepanel.SetActive(true);
            addtionalpowerup.SetActive(true);
            StartCoroutine(DestroypowerUpNotice());
        }

        // When to Start powerUp in maze 2
        if (gameUI.maintime.minutes == maze2activatetime && levelmanager.currentmaze == 2 && !levelmanager.maze2completed)
        {
            startpowerup1 = true;
            startpowerup2 = true;
            Debug.Log("Additional Power Up Activated");

            additionalpower1update.text = "";
            additionalpower2update.text = "";

            middlepanel.SetActive(true);
            addtionalpowerup.SetActive(true);
            StartCoroutine(DestroypowerUpNotice());
        }

        // When to Start powerUp in maze 3
        if (gameUI.maintime.minutes == maze3activatetime && levelmanager.currentmaze == 3 && !levelmanager.maze3completed)
        {
            startpowerup1 = true;
            startpowerup2 = true;
            Debug.Log("Additional Power Up Activated");

            additionalpower1update.text = "";
            additionalpower2update.text = "";

            middlepanel.SetActive(true);
            addtionalpowerup.SetActive(true);
            StartCoroutine(DestroypowerUpNotice());
        }

    }

    IEnumerator DestroypowerUpNotice()
    {
        yield return new WaitForSeconds(gameUI.uiwaittime);
        addtionalpowerup.SetActive(false);
        addtionalpowerup2.SetActive(true);
    }

    void WhenToStopPowerUp()
    {
        //If in Maze 1 and powerup is Activated
        if(levelmanager.currentmaze == 1)
        {
            if(maze1power1count <= 0 && startpowerup1)
            {
                startpowerup1 = false;
                onceamaze = false;       // This Ability allowed only once a maze
                Debug.Log("Additional Power Up Deactivated");
                additionalpower1update.text = "Power Deactivated";
            }
            if (maze1power2count <= 0 && startpowerup2)
            {
                startpowerup2 = false;
                onceamaze = false;       // This Ability allowed only once a maze
                Debug.Log("Additional Power Up Deactivated");
                additionalpower2update.text = "Power Deactivated";
            }
        }

        //If in Maze 2 and powerup is Activated
        if (levelmanager.currentmaze == 2)
        {
            if (maze2power1count <= 0 && startpowerup1)
            {
                startpowerup1 = false;
                onceamaze = false;       // This Ability allowed only once a maze
                Debug.Log("Additional Power Up Deactivated");
                additionalpower1update.text = "Power Deactivated";
            }
            if (maze2power2count <= 0 && startpowerup2)
            {
                startpowerup2 = false;
                onceamaze = false;       // This Ability allowed only once a maze
                Debug.Log("Additional Power Up Deactivated");
                additionalpower2update.text = "Power Deactivated";
            }
        }

        //If in Maze 3 and powerup is Activated
        if (levelmanager.currentmaze == 3)
        {
            if (maze3power1count <= 0 && startpowerup1)
            {
                startpowerup1 = false;
                onceamaze = false;       // This Ability allowed only once a maze
                Debug.Log("Additional Power Up Deactivated");
                additionalpower1update.text = "Power Deactivated";
            }
            if (maze3power2count <= 0 && startpowerup2)
            {
                startpowerup2 = false;
                onceamaze = false;       // This Ability allowed only once a maze
                Debug.Log("Additional Power Up Deactivated");
                additionalpower2update.text = "Power Deactivated";
            }
        }


        // UI Deactivating
        if(gameUI.maintime.minutes >= maze1activatetime && !startpowerup1 && !startpowerup2 && levelmanager.currentmaze == 1)
        {
            StartCoroutine(CloseUIPowerUp());
        }
        if (gameUI.maintime.minutes >= maze2activatetime && !startpowerup1 && !startpowerup2 && levelmanager.currentmaze == 2)
        {
            StartCoroutine(CloseUIPowerUp());
        }
        if (gameUI.maintime.minutes >= maze3activatetime && !startpowerup1 && !startpowerup2 && levelmanager.currentmaze == 3)
        {
            StartCoroutine(CloseUIPowerUp());
        }

    }

    IEnumerator CloseUIPowerUp()
    {
        yield return new WaitForSeconds(5);
        additionalpower1update.text = "";
        additionalpower2update.text = "";
        middlepanel.SetActive(false);
    }


    IEnumerator ClosePath()
    {
        yield return new WaitForSeconds(pathtime);
        showpath = false;

        maze2path.SetActive(false);
        maze1path.SetActive(false);
        maze3path.SetActive(false);

    }

    IEnumerator ShowMaze()
    {
        yield return new WaitForSeconds(waittime);
        originalmaze2.SetActive(true);
        originalmaze3.SetActive(true);
        originalmaze1.SetActive(true);

        playerController.motionrestricted = false;
        seethroughtaken = false;
    }

}
