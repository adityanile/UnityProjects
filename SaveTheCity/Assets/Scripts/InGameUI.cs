using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameUI : MonoBehaviour
{
    private LevelManager levelManager;  // Reference to Level manager script on player
    private SafeSpotManager safeSpotManager; // Reference to SafeSpotmanager Script on player

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


    private void Awake()
    {
        // Initialise timer
        maintime.seconds = 0;
        maintime.minutes = 0;

        levelManager = GameObject.Find("Player").GetComponent<LevelManager>();
        safeSpotManager = GameObject.Find("Player").GetComponent<SafeSpotManager>();

        leftpanel.SetActive(false);
        maze1time.text = "";
        maze2time.text = "";
        maze3time.text = "";

        mazewallcollision.SetActive(false);
        outerwallcollision.SetActive(false);

        cyrstalcollected.text = "";
         
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

        maintime.seconds += Time.deltaTime;

        while(maintime.seconds >= 60) { 
            maintime.seconds -= 60;
            maintime.minutes++;
        }

        maintimer.text = "Time :- " + maintime.minutes + ":" + Convert.ToInt16( maintime.seconds );
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

        }
        if (levelManager.maze2completed && !updatedtimemaze2)
        {
            updatedtimemaze2 = true;
            maze2time.text = "2nd Maze Completed in :- " + maintime.minutes + ":" + Convert.ToInt16(maintime.seconds);

            // Reset the clock
            maintime.seconds = 0;
            maintime.minutes = 0;

        }
        if (levelManager.maze3completed && !updatedtimemaze3)
        {
            updatedtimemaze3 = true;
            maze3time.text = "3rd Maze Completed in :- " + maintime.minutes + ":" + Convert.ToInt16(maintime.seconds);

            // Reset the clock
            maintime.seconds = 0;
            maintime.minutes = 0;

        }
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
            levelManager.crystalcollected = false;
            leftpanel.SetActive(true);
            cyrstalcollected.text = "Congrats ! " + levelManager.crystalsCollected + "  / 4 Crystals Collected";
            StartCoroutine(DestroyCrystalUI());
        }

        if (levelManager.allCrystalsCollected && !doneonce)
        {
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


}

public class GameTime
{
    public int minutes;
    public float seconds;
} 

