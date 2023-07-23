using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameUI : MonoBehaviour
{
    private LevelManager levelManager;  // Reference to Level manager script on player
    private SafeSpotManager safeSpotManager; // Reference to SafeSpotmanager Script on player
    private PlayerController playerController;  // When to Start the game

    public GameObject leftpanel;
    public GameObject rightpanel;
    public GameObject middlepanel;

    public int uiwaittime = 10;  // Wait for 10sec for before closing ui


    // All Timer Management
    public TextMeshProUGUI maintimer;
    public GameTime maintime = new GameTime();
    public TextMeshProUGUI maze1time;
    public TextMeshProUGUI maze2time;
    public TextMeshProUGUI maze3time;

    public bool updatedtimemaze1 = false;
    public bool updatedtimemaze2 = false;
    public bool updatedtimemaze3 = false;

    // Wall collision UI
    public GameObject mazewallcollision;
    public GameObject outerwallcollision;

    public GameObject safespottext;

    // Crystal Collection update
    public TextMeshProUGUI cyrstalcollected;
    public GameObject allcrystalscollected;
    public bool doneonce = false;

    // maze Completed UI's
    public GameObject mainpanel;
    public TextMeshProUGUI mazecompleted;

    // Power UP UI
    public GameObject powerupupdate;
    public GameObject powerupuse;
    public GameObject speeduptaken;
    public GameObject jumpuptaken; 
    public GameObject fireballtaken;

    public GameObject gamecompleted;
    private void Awake()
    {
        // Initialise timer
        maintime.seconds = 0;
        maintime.minutes = 0;

        levelManager = GameObject.Find("Player").GetComponent<LevelManager>();
        safeSpotManager = GameObject.Find("Player").GetComponent<SafeSpotManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        leftpanel.SetActive(false);
        maze1time.text = "";
        maze2time.text = "";
        maze3time.text = "";

        mazewallcollision.SetActive(false);
        outerwallcollision.SetActive(false);
        gamecompleted.SetActive(false);

        cyrstalcollected.text = "";
        mazecompleted.text = "";
        mainpanel.SetActive(false); 
    }

    void Start()
    {
        StartCoroutine( DisplayNotice() );    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(); // Update Maintime
        CheckMazeCompleted();  // To Update particular maze time
  
        WallCollisonUI();  // Checks collision with every wall
        SafeSpotUpdate();

        CheckCystalCollected();
    }


    void UpdateTime()
    {
        if (playerController.startgame)  // if Game is Started then only start the time
        {
            maintime.seconds += Time.deltaTime;

            while (maintime.seconds >= 60)
            {
                maintime.seconds -= 60;
                maintime.minutes++;
            }

            maintimer.text = "Time :- " + maintime.minutes + ":" + Convert.ToInt16(maintime.seconds);
        }
    }

    // Give Notice to Read Info after 5sec of Game Starting
    IEnumerator DisplayNotice()
    {
        yield return new WaitForSeconds(5);
        mainpanel.SetActive(true);
        mazecompleted.text = "To Understand The Game Completed \n GoTo The Portals Present Around You";
        StartCoroutine( StopNotice() );
    } 

    IEnumerator StopNotice()
    {
        yield return new WaitForSeconds(uiwaittime);
        mainpanel.SetActive(false);
        mazecompleted.text = "";
    }


   void CheckMazeCompleted()
    {
        if (levelManager.maze1completed && !updatedtimemaze1)
        {
            updatedtimemaze1 = true;
            maze1time.text = "1st Maze Completed in :- " + maintime.minutes + ":" + Convert.ToInt16(maintime.seconds);

            // Reset the clock
            maintime.seconds = 0;
            maintime.minutes = 0;

            // UI Update 
            mainpanel.SetActive(true);
            mazecompleted.text = "Congrats !! For Completing Maze 1";
            StartCoroutine(DestroyMazeUpdate());

        }
        if (levelManager.maze2completed && !updatedtimemaze2)
        {
            updatedtimemaze2 = true;
            maze2time.text = "2nd Maze Completed in :- " + maintime.minutes + ":" + Convert.ToInt16(maintime.seconds);

            // Reset the clock
            maintime.seconds = 0;
            maintime.minutes = 0;

            // UI Update 
            mainpanel.SetActive(true);
            mazecompleted.text = "Congrats !! For Completing Maze 2";
            StartCoroutine(DestroyMazeUpdate());

        }
        if (levelManager.maze3completed && !updatedtimemaze3)
        {
            updatedtimemaze3 = true;
            maze3time.text = "3rd Maze Completed in :- " + maintime.minutes + ":" + Convert.ToInt16(maintime.seconds);

            // Reset the clock
            maintime.seconds = 0;
            maintime.minutes = 0;

            // UI Update 
            mainpanel.SetActive(true);
            mazecompleted.text = "Congrats !! For Completing Maze 3";
            StartCoroutine(DestroyMazeUpdate());

        }
    }

    // All PowerUps Updates

    void PowerUpUIUpdates()
    {
        leftpanel.SetActive(true);
        powerupupdate.SetActive(true);
        safespottext.SetActive(false);
        StartCoroutine(LaterPowerUpUIUpdate());   // Middle panel Update
        StartCoroutine(DestroyPowerUp());
    }

    IEnumerator LaterPowerUpUIUpdate()
    {
        yield return new WaitForSeconds(3);
        mainpanel.SetActive(true);
        powerupuse.SetActive(true);
        StartCoroutine(DestroyLaterPowerUp());
    }

    IEnumerator DestroyLaterPowerUp() {
        yield return new WaitForSeconds(uiwaittime);
        mainpanel.SetActive(false);
        powerupuse.SetActive(false);
    }

    IEnumerator DestroyPowerUp()
    {
        yield return new WaitForSeconds(uiwaittime);
        leftpanel.SetActive(false);
        powerupupdate.SetActive(false);
    }

    IEnumerator DestroyMazeUpdate()
    {
        yield return new WaitForSeconds(uiwaittime / 2);
        mainpanel.SetActive(false);
        mazecompleted.text = "";

        if (levelManager.maze1completed && !levelManager.maze2completed && !levelManager.maze3completed)
        {
            PowerUpUIUpdates();
        }

    }

    public void SpeedUpTakenUpdate()
    {
        mainpanel.SetActive(true);
        speeduptaken.SetActive(true);
        StartCoroutine(StopPowerTakenUI());
    }
    public void JumpUpTakenUpdate()
    {
        mainpanel.SetActive(true);
        jumpuptaken.SetActive(true);
        StartCoroutine(StopPowerTakenUI());
    }
    public void FireBallTakenUpdate()
    {
        mainpanel.SetActive(true);
        fireballtaken.SetActive(true);
        StartCoroutine(StopPowerTakenUI());
    }

    IEnumerator StopPowerTakenUI()
    {
        yield return new WaitForSeconds(2);
        mainpanel.SetActive(false);
        speeduptaken.SetActive(false);
        jumpuptaken.SetActive(false);
        fireballtaken.SetActive(false);
    }

    void WallCollisonUI()
    {
        if (safeSpotManager.collidedwithmazewall)
        {
            safeSpotManager.collidedwithmazewall = false;

            safespottext.SetActive(false);

            leftpanel.SetActive(true);
            mazewallcollision.SetActive(true);
            StartCoroutine(DestroyUI());
        }
        if (safeSpotManager.collidedwithouterwall)
        {
            safeSpotManager.collidedwithouterwall = false;

            safespottext.SetActive(false);

            leftpanel.SetActive(true);
            outerwallcollision.SetActive(true);
            StartCoroutine(DestroyUI());
        }
    }

    void SafeSpotUpdate()
    {
        if (safeSpotManager.safespotcollected)
        {
            safeSpotManager.safespotcollected = false;

            // To avoid collision between UI
            mazewallcollision.SetActive(false);
            outerwallcollision.SetActive(false);

            leftpanel.SetActive(true);
            safespottext.SetActive(true);
            StartCoroutine(DestroyUI());
        }
    }

    void CheckCystalCollected()
    {
        if (levelManager.crystalcollected)
        {
            mazewallcollision.SetActive(false);
            outerwallcollision.SetActive(false);
            safespottext.SetActive(false) ;

            levelManager.crystalcollected = false;
            leftpanel.SetActive(true);
            cyrstalcollected.text = "Congrats ! " + levelManager.crystalsCollected + "  / 4 Crystals Collected";
            StartCoroutine(DestroyCrystalUI());
        }

        if (levelManager.allCrystalsCollected && !doneonce)
        {
            mazewallcollision.SetActive(false);
            outerwallcollision.SetActive(false);

            levelManager.allCrystalsCollected = false;
            doneonce = true;
            leftpanel.SetActive(true);
            cyrstalcollected.text = "";
            allcrystalscollected.SetActive(true);
            StartCoroutine(DestroyCrystalUI());
        }
    }

    IEnumerator DestroyCrystalUI()
    {
        yield return new WaitForSeconds(uiwaittime);
        cyrstalcollected.text = "";
        leftpanel.SetActive(false);
        allcrystalscollected.SetActive(false);
    }


    IEnumerator DestroyUI()
    {
        yield return new WaitForSeconds(uiwaittime);

        safespottext.SetActive(false);
        mazewallcollision.SetActive(false);
        outerwallcollision.SetActive(false);
        leftpanel.SetActive(false);

    }

    public IEnumerator WinnerBaseUIUpdate()
    {
        yield return new WaitForSeconds(6);
        mainpanel.SetActive(true);
        gamecompleted.SetActive(true);

        StartCoroutine(DestroyWinnerBaseUpdate());
    }
    IEnumerator DestroyWinnerBaseUpdate()
    {
        yield return new WaitForSeconds(4);
        mainpanel.SetActive(false);
        gamecompleted.SetActive(false);
    }
}

public class GameTime
{
    public int minutes;
    public float seconds;
} 

