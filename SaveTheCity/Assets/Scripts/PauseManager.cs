using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseui;
    private bool gamepaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Setting Pause in PlayerController

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamepaused)
            {
                Time.timeScale = 0;    // The game when pressed Escape
                pauseui.SetActive(true);
                gamepaused = true;
            }
            else
            {
                gamepaused = false;
                pauseui.SetActive(false);
                Time.timeScale = 1;
            }

            

        }

       

    }

    public void OnClickResume()
    {
        Time.timeScale = 1;          // Continue the game
        pauseui.SetActive(false);
               
    }

    public void OnClickExit()
    {
        Application.Quit();     // Quit the Application
    }

}
