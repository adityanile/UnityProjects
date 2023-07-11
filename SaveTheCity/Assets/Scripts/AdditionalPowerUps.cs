using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        maze1path.SetActive(false);
        maze2path.SetActive(false);
        maze3path.SetActive(false);

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

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !seethroughtaken)
        {
            seethroughtaken = true;
            playerController.motionrestricted = true;

            if (levelmanager.currentmaze == 1)
            {
                StartCoroutine(ShowMaze());
                originalmaze2.SetActive(false);
            }
            else if(levelmanager.currentmaze == 2)
            {
                StartCoroutine (ShowMaze());
                originalmaze3.SetActive(false);
            }
            else if(levelmanager.currentmaze == 3)
            {
                StartCoroutine(ShowMaze());
                originalmaze1.SetActive(false);
            }

        }

        if (Input.GetKeyDown(KeyCode.P) && !showpath)
        {
            showpath = true;

            if (levelmanager.currentmaze == 1)
            {
                StartCoroutine(ClosePath());
                maze1path.SetActive(true);
            }
            else if (levelmanager.currentmaze == 2)
            {
                StartCoroutine(ClosePath());
                maze2path.SetActive(true);
            }
            else if (levelmanager.currentmaze == 3)
            {
                StartCoroutine(ClosePath());
                maze3path.SetActive(true);
            }
        }


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
