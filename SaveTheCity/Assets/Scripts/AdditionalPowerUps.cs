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
    public int waittime = 7;

    private PlayerController playerController;
    private bool seethroughtaken = false;

    private GameObject maze2path;
    private bool showpath = false;
    private int pathtime = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        maze2path = GameObject.Find("Path");

        maze2path.SetActive(false);
        originalmaze2 = GameObject.Find("OriginalMaze2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !seethroughtaken)
        {
            seethroughtaken = true;

            playerController.motionrestricted = true;
            StartCoroutine(ShowMaze());   
            originalmaze2.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P) && !showpath)
        {
            showpath = false;
            maze2path.SetActive(true);
            StartCoroutine(ClosePath());

        }


    }

    IEnumerator ClosePath()
    {
        yield return new WaitForSeconds(pathtime);
        maze2path.SetActive(false);
    }

    IEnumerator ShowMaze()
    {
        yield return new WaitForSeconds(waittime);
        originalmaze2.SetActive(true);
        playerController.motionrestricted = false;
        seethroughtaken = false;
    }

}
