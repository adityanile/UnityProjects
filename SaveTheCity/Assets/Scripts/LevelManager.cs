using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Every Crysta You Acquire Gives you magical power in the form of powerUps


    public int currentLevel = 1;
    public int crystalsCollected = 0;

    public GameObject player;
    public bool levelUpgraded = false;
    private bool allCrystalsCollected = false;

    // For Level 1
    public GameObject crystal1Level1;
    public GameObject crystal2Level1;
    public GameObject crystal3Level1;
    public GameObject crystal4Level1;
    public GameObject level1Key;
    

    // Start is called before the first frame update
    void Start()
    {
        level1Key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(crystalsCollected >= 4)
        {
            allCrystalsCollected = true;
        }
        if (allCrystalsCollected)
        {
            Debug.Log("Key Unlocked At the End");
            level1Key.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Level 1 All Collisions
        if (collision.gameObject.CompareTag("crystal1Level1"))
        {
            crystalsCollected++;
            Destroy(crystal1Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }
        if (collision.gameObject.CompareTag("crystal2Level1"))
        {
            crystalsCollected++;
            Destroy(crystal2Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }
        if (collision.gameObject.CompareTag("crystal3Level1"))
        {
            crystalsCollected++;
            Destroy(crystal3Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }
        if (collision.gameObject.CompareTag("crystal4Level1"))
        {
            crystalsCollected++;
            Destroy(crystal4Level1);
            Debug.Log("Crystals Collected:- " + crystalsCollected);
        }

        // Collision with Key
        if (collision.gameObject.CompareTag("level1Key"))
        {
            level1Key.SetActive(false);
            player.transform.position = new Vector3(-511, transform.position.y, 407);
            currentLevel++;
            levelUpgraded = true;
            Debug.Log("Curren Level:- " +currentLevel);
        }

    }

}
