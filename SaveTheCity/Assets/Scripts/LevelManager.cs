using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Every Crysta You Acquire Gives you magical power in the form of powerUps
    public int crystalsCollected = 0;       // This is the count to display in the UI

    private PowerUps powerUps;  // Get Power ups Script on player

    //Game Music Control
    private GameMusic gameMusic;

    // Separate count for activation of keys
    public int crystalcountlevel1 = 0;
    public int crystalcountlevel2 = 0;
    public int crystalcountlevel3 = 0;

    public bool crystalcollected = false;

    public GameObject player;
    public bool levelUpgraded = false;
    public bool allCrystalsCollected = false;

    private AdditionalPowerUps additionalpowerups;
    private InGameUI gameUI;
    private SafeSpotManager safeSpot;
    
    public GameObject level1Key;   // Takes you level 3 from 1
    private GameObject level2key;  // Takes you level 2 from 1
    private GameObject level3key;  // Takes you level 3 from 2
    private GameObject level2key2; // Takes you level 2 from 3

    public bool maze1completed = false;
    public bool maze2completed = false;
    public bool maze3completed = false;

    private GameObject level1backkey;
    private GameObject level2backkey;
    public bool backkey1active = true;
    public bool backkey2active = true;

    public int currentmaze = 0;

    GameObject ropebridgelevel1;

    public GameObject winnerbase;
    public bool alreadyattheend = false;
    private bool winnerdoneonce = false;

    public AudioSource playaudio;
    public AudioClip cryatalcollected;
    public AudioClip portaltravel;
    public AudioClip mazecomplete;
    public AudioClip allmazecompleted;
    public AudioClip notice;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = GameObject.Find("Player").GetComponent<PowerUps>();
        gameUI = GameObject.Find("UIManager").GetComponent<InGameUI>();
        safeSpot = GameObject.Find("Player").GetComponent<SafeSpotManager>();
        gameMusic = GameObject.Find("GameAudio").GetComponent<GameMusic>();
        playaudio = GetComponent<AudioSource>();

        if (gameObject.CompareTag("Player"))
        {
            ropebridgelevel1.SetActive(false);
            level2key.SetActive(false);
            level3key.SetActive(false);
            level2key2.SetActive(false);
        }
        level1Key.SetActive(false);
       
    }

    void Awake()
    {
        ropebridgelevel1 = GameObject.Find("ropebridgelevel1");
        level2key = GameObject.Find("level2Key");
        level2backkey = GameObject.Find("level2BackKey");
        level1backkey = GameObject.Find("level1BackKey");

        level3key = GameObject.Find("level3Key");
        level2key2 = GameObject.Find("level2Key2");
        winnerbase = GameObject.Find("WinnerBase");

        additionalpowerups = GameObject.Find("AdditionalPowerUps").GetComponent<AdditionalPowerUps>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.CompareTag("Player"))
        {

            if (crystalcountlevel1 == 4)
            {
                allCrystalsCollected = true;
            }

            if (allCrystalsCollected)
            {
                ropebridgelevel1.SetActive(true);
                level2key.SetActive(true);
                level1Key.SetActive(true);
            }

            if (crystalcountlevel2 >= 4)
            {
                crystalcountlevel2 = 0;
                allCrystalsCollected = true;

                // Active key form level 2 to level 3 
                level3key.SetActive(true);
            }

            if(crystalcountlevel3 >= 4)
            {
                crystalcountlevel3 = 0;     // Setting again to zero to avoid the loop
                allCrystalsCollected = true;

                // Active key form level 3 to level 2 
                level2key2.SetActive(true);
            }

            if (maze1completed && maze2completed && maze3completed && !alreadyattheend)
            {
                alreadyattheend = true;

                gameUI.leftpanel.SetActive(false);
                gameUI.middlepanel.SetActive(false);
                // What will happen when All maze are completed
                currentmaze = 0;
                crystalsCollected = 12;

                winnerbase.SetActive(true);
                transform.position = new Vector3(-1057, transform.position.y, 32);

                if (!winnerdoneonce)
                {
                    winnerdoneonce = true;
                    StartCoroutine(gameUI.WinnerBaseUIUpdate());  // Start Last Ui Update when enter winner base
                }
                gameUI.isgamecompleted = true;   // Set game completed true to stop total time
                Debug.Log("Total Time Taken:- " + gameUI.totaltime.minutes + ":" + gameUI.totaltime.seconds);
            }
        }

        if (backkey1active || backkey2active)
        {
            // Destroy Back Key After Collecting one Crystal
            if (crystalcountlevel2 == 1)
            {
                backkey1active = false;
                level2backkey.SetActive(false);
            }
            if (crystalcountlevel3 == 1)
            {
                backkey2active = false;
                level1backkey.SetActive(false);
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {  
        // Allowing only player to control keys
        if (gameObject.CompareTag("Player"))
        {
            // Collision with Key1
            if (collision.gameObject.CompareTag("level1Key"))
            {
                // Time Management
                gameUI.maze1time.text = "1st Maze Completed in :- " + gameUI.maintime.minutes + ":" + Convert.ToInt16(gameUI.maintime.seconds);
                gameUI.ResetClock();

                gameMusic.Maze3Audio();   // Start Maze 3 Audio
                playaudio.PlayOneShot(portaltravel, 1);    // Portal Travel Sound
                crystalsCollected = 0;
                crystalcountlevel1 = 0;
                maze1completed = true;

                // Activate one speedup , jumpup
                powerUps.maxJumpUps = 1;
                powerUps.maxSpeedUps = 1;

                additionalpowerups.DestroyPowerUpUI();

                // Even After taking the key , key will not be destroyed
                player.transform.position = new Vector3(-511, transform.position.y, 407);  // teleport to maze3
                levelUpgraded = true;
                currentmaze = 3;

                allCrystalsCollected = false;

                // Activate Level1 backkey
                level1backkey.SetActive(true);

                // Setting SafeSpot position
                safeSpot.safePosition = gameObject.transform.position; 
            }

            if (collision.gameObject.CompareTag("level2Key"))
            {
                // Time Management
                gameUI.maze1time.text = "1st Maze Completed in :- " + gameUI.maintime.minutes + ":" + Convert.ToInt16(gameUI.maintime.seconds);
                gameUI.ResetClock();

                gameMusic.Maze2Audio();       // Start Audio
                playaudio.PlayOneShot(portaltravel, 1);    // Portal Travel Sound

                crystalsCollected = 0;
                crystalcountlevel1 = 0;
                maze1completed = true;

                // Activate one speedup , jumpup
                powerUps.maxJumpUps = 1;
                powerUps.maxSpeedUps = 1;

                allCrystalsCollected = false;

                additionalpowerups.DestroyPowerUpUI();  

                // Even After taking the key , key will not be destroyed
                player.transform.position = new Vector3(-657, transform.position.y, -1007);   // Teleport to maze2
                currentmaze = 2;

                // Set level2 back key active
                level2backkey.SetActive(true);
                levelUpgraded = true;

                // Setting SafeSpot position
                safeSpot.safePosition = gameObject.transform.position;
            }

            // Two Additional Keys mangement

            if (collision.gameObject.CompareTag("level3key"))  // This is the key at the end of maze 2 to maze 3
            {
                // Time Management
                gameUI.maze2time.text = "2nd Maze Completed in :- " + gameUI.maintime.minutes + ":" + Convert.ToInt16(gameUI.maintime.seconds);
                gameUI.ResetClock();

                gameMusic.Maze3Audio();   // Start Maze 3 Audio
                playaudio.PlayOneShot(portaltravel, 1);    // Portal Travel Sound

                level2backkey.SetActive(true);   // Set The back key active that we set deactive at the start

                crystalsCollected = 0;
                maze2completed = true;
                additionalpowerups.DestroyPowerUpUI();

                // Even After taking the key , key will not be destroyed
                player.transform.position = new Vector3(-511, transform.position.y, 407);  // teleport to maze3
                levelUpgraded = true;
                currentmaze = 3;

                // Setting SafeSpot position
                safeSpot.safePosition = gameObject.transform.position;
            }

            if (collision.gameObject.CompareTag("level2key2"))  // This is the key at the end of maze 3 to maze 2
            {
                // Time Management
                gameUI.maze3time.text = "3rd Maze Completed in :- " + gameUI.maintime.minutes + ":" + Convert.ToInt16(gameUI.maintime.seconds);
                gameUI.ResetClock();
                gameMusic.Maze2Audio();       // Start Audio
                playaudio.PlayOneShot(portaltravel, 1);    // Portal Travel Sound

                level1backkey.SetActive(true);   // Set The back key active that we set deactive at the start

                crystalsCollected = 0;
                maze3completed = true;
                additionalpowerups.DestroyPowerUpUI();

                // Even After taking the key , key will not be destroyed
                player.transform.position = new Vector3(-657, transform.position.y, -1007);   // Teleport to maze2
                currentmaze = 2;

                // Setting SafeSpot position
                safeSpot.safePosition = gameObject.transform.position;
            }

            // Managing Back Keys
            if (collision.gameObject.CompareTag("Level1BackKey"))
            {
                playaudio.PlayOneShot(portaltravel, 1);    // Portal Travel Sound

                crystalsCollected = 0;
                Debug.Log("Crystals Collected:- " + crystalsCollected);

                player.transform.position = new Vector3(-613.7f, 34.7f, -340.2f);
            }
            if (collision.gameObject.CompareTag("Level2BackKey"))
            {
                playaudio.PlayOneShot(portaltravel, 1);    // Portal Travel Sound

                crystalsCollected = 0;
                Debug.Log("Crystals Collected:- " + crystalsCollected);

                player.transform.position = new Vector3(-613.7f, 34.7f, -340.2f);
            }


            // Crystals Managemnet

            if (collision.gameObject.CompareTag("Level1Crystal"))
            {
                playaudio.PlayOneShot(cryatalcollected, 1);

                crystalcollected = true;
                crystalcountlevel1++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
            if (collision.gameObject.CompareTag("Level2Crystal"))
            {
                playaudio.PlayOneShot(cryatalcollected, 1);

                crystalcollected = true;
                crystalcountlevel2++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
            if (collision.gameObject.CompareTag("Level3Crystal"))
            {
                playaudio.PlayOneShot(cryatalcollected, 1);

                crystalcollected = true;
                crystalcountlevel3++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
        }
    }
}
