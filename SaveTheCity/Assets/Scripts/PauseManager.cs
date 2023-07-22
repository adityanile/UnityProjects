using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseui;
    private bool gamepaused = false;

    private LevelManager levelManager;
    private PowerUps powerUps;
    private SafeSpotManager spotManager;

    public TextMeshProUGUI currenmaze;
    public TextMeshProUGUI crystalscollected;
    public TextMeshProUGUI powerupstatus;

    public GameObject helpbutton;
    public GameObject helptext;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Player").GetComponent<LevelManager>();
        powerUps = GameObject.Find("Player").GetComponent<PowerUps>();
        spotManager = GameObject.Find("Player").GetComponent<SafeSpotManager>();

        pauseui.SetActive(false);
        currenmaze.text = "";
        powerupstatus.text = "";

        helptext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamepaused)
            {
                Time.timeScale = 0;    // The game when pressed Escape
                pauseui.SetActive(true);
                gamepaused = true;

                UIUpdatesInPauseMenu();
            }
            else
            {
                gamepaused = false;
                pauseui.SetActive(false);
                Time.timeScale = 1;
            }
        }    
    }

    void UIUpdatesInPauseMenu()
    {
        currenmaze.text = "Current Maze:- " + levelManager.currentmaze;
        crystalscollected.text = "Crystals Collected:- " + levelManager.crystalsCollected;

        if (levelManager.maze1completed)    // If Maze 1 completed then only show status of PowerUps
        {
            powerupstatus.text = "Power Up Status:- \n\nSpeed Up :- " + powerUps.maxSpeedUps +
                                 "\nJump Up :- " + powerUps.maxJumpUps +
                                 "\nFire Ball :- " + spotManager.fireballCount;
        }
    }

    public void OnClickHelp()
    {
        helpbutton.SetActive(false);
        helptext.SetActive(true);
    }

    public void OnClickResume()
    {
        gamepaused = false;
        Time.timeScale = 1;          // Continue the game
        pauseui.SetActive(false);
               
    }

    public void OnClickExit()
    {
        Application.Quit();     // Quit the Application
    }

}
