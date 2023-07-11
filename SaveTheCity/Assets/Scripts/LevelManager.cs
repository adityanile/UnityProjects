using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Every Crysta You Acquire Gives you magical power in the form of powerUps

    public int crystalsCollected = 0;       // This is the count to display in the UI

    // Separate count for activation of keys
    public int crystalcountlevel1 = 0;
    public int crystalcountlevel2 = 0;
    public int crystalcountlevel3 = 0;


    public GameObject player;
    public bool levelUpgraded = false;
    public bool allCrystalsCollected = false;

    // For Level 1
    public GameObject crystal1Level1;
    public GameObject crystal2Level1;
    public GameObject crystal3Level1;
    public GameObject crystal4Level1;

    public GameObject level1Key;   // Takes you level 3 from 1
    private GameObject level2key;  // Takes you level 2 from 1
    private GameObject level3key;  // Takes you level 3 from 2
    private GameObject level2key2; // Takes you level 2 from 3

    public bool maze1completed = false;
    public bool maze2completed = false;
    public bool maze3completed = false;

    private GameObject level1backkey;
    private GameObject level2backkey;

    public int currentmaze = 1;

     GameObject ropebridgelevel1;

    // Start is called before the first frame update
    void Start()
    {
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

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.CompareTag("Player"))
        {

            if (crystalcountlevel1 >= 4)
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
                maze2completed = true;
                level3key.SetActive(true);
            }

            if(crystalcountlevel3 >= 4)
            {
                // Active key form level 3 to level 2 
                maze3completed = true;
                level2key2.SetActive(true);
            }

        }

        if(maze1completed && maze2completed && maze3completed)
        {
            // What will happen when All maze are completed
        }


    }


    void OnCollisionEnter(Collision collision)
    {
        // Level 1 All Collisions
        if (collision.gameObject.CompareTag("crystal1Level1"))
        {
            crystalcountlevel1++;
            crystalsCollected++;
            Destroy(crystal1Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }
        if (collision.gameObject.CompareTag("crystal2Level1"))
        {
            crystalcountlevel1++;
            crystalsCollected++;
            Destroy(crystal2Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }
        if (collision.gameObject.CompareTag("crystal3Level1"))
        {
            crystalcountlevel1++;
            crystalsCollected++;
            Destroy(crystal3Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }
        if (collision.gameObject.CompareTag("crystal4Level1"))
        {
            crystalcountlevel1++;
            crystalsCollected++;
            Destroy(crystal4Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }

        // Allowing only player to control keys
        if (gameObject.CompareTag("Player"))
        {
            // Collision with Key1
            if (collision.gameObject.CompareTag("level1Key"))
            {
                crystalsCollected = 0;     // Restart Crystal Collection
                Debug.Log("Crystals Collected:- " + crystalsCollected);

                maze1completed = true;

                // Even After taking the key , key will not be destroyed
                player.transform.position = new Vector3(-511, transform.position.y, 407);  // teleport to maze3
                levelUpgraded = true;
                currentmaze = 3;

                // Activate Level1 backkey
                level1backkey.SetActive(true);
            }

            if (collision.gameObject.CompareTag("level2Key"))
            {

                crystalsCollected = 0;     // Restart Crystal Collection
                Debug.Log("Crystals Collected:- " + crystalsCollected);

                maze1completed = true;

                // Even After taking the key , key will not be destroyed
                player.transform.position = new Vector3(-657, transform.position.y, -1007);   // Teleport to maze2

                // Set level2 back key active
                level2backkey.SetActive(true);
                currentmaze = 2;

                levelUpgraded = true;
            }

            // Managing Back Keys

            if (collision.gameObject.CompareTag("Level1BackKey"))
            {
                crystalsCollected = 0;     // Restart Crystal Collection
                Debug.Log("Crystals Collected:- " + crystalsCollected);

                player.transform.position = new Vector3(-613.7f, 34.7f, -340.2f);
            }
            if (collision.gameObject.CompareTag("Level2BackKey"))
            {
                crystalsCollected = 0;     // Restart Crystal Collection
                Debug.Log("Crystals Collected:- " + crystalsCollected);

                player.transform.position = new Vector3(-613.7f, 34.7f, -340.2f);
            }


            // Crystals Managemnet of level 2

            if (collision.gameObject.CompareTag("Crystal1Level2"))
            {
                crystalcountlevel2++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }

            if (collision.gameObject.CompareTag("Crystal2Level2"))
            {
                crystalcountlevel2++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
            if (collision.gameObject.CompareTag("Crystal3Level2"))
            {
                crystalcountlevel2++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
            if (collision.gameObject.CompareTag("Crystal4Level2"))
            {
                crystalcountlevel2++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }

            // Managing Level 3 Crystals

            if (collision.gameObject.CompareTag("Crystal1Level3"))
            {
                crystalcountlevel3++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }

            if (collision.gameObject.CompareTag("Crystal2Level3"))
            {
                crystalcountlevel3++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
            if (collision.gameObject.CompareTag("Crystal3Level3"))
            {
                crystalcountlevel3++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }
            if (collision.gameObject.CompareTag("Crystal4Level3"))
            {
                crystalcountlevel3++;
                crystalsCollected++;
                Destroy(collision.gameObject);
                Debug.Log("Crystals Collected:- " + crystalsCollected);
            }


        }

    }

}
