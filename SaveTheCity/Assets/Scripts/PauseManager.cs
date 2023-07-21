using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseui;
    private bool gamepaused = false;

    private LevelManager levelManager;

    public TextMeshProUGUI currenmaze;
    public TextMeshProUGUI crystalscollected;

    public GameObject helpbutton;
    public GameObject helptext;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Player").GetComponent<LevelManager>();
        pauseui.SetActive(false);
        currenmaze.text = "";

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
