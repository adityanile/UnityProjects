using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpotManager : MonoBehaviour
{
    // Level1 SafeSpots
    public GameObject sp1Level1;
    public GameObject sp2Level1;
    public GameObject sp3Level1;
    public GameObject sp4Level1;


    // Level 3 SafeSpots
    public GameObject spot1;
    public GameObject spot2;
    public GameObject spot3;
    public GameObject spot4;
    public GameObject spot5;
    public GameObject spot6;
    public GameObject spot7;
    public GameObject spot8;

    private GameObject sp1level3;
    private GameObject sp2level3;
    private GameObject sp3level3;
    private GameObject sp4level3;
    private GameObject sp5level3;

    public GameObject player;

    public ParticleSystem fireBallEffect;

    public Vector3 safePosition = new Vector3(0,0.9f,0);

    private PlayerController setflags;
    private PowerUps fireballController;



    // Fire Ball Manager

    private bool fireballTaken = false;
    public GameObject fireball1;
    public GameObject fireball2;
    private int fireballCount = 0;

    // Managing Wall Explosion
    private ParticleSystem wallExplosion;
    bool wallExplosionOn = false;
    float coolDown = 3;

    // Start is called before the first frame update
    void Start()
    {
        setflags = GameObject.Find("Player").GetComponent<PlayerController>();
        fireballController = GameObject.Find("PowerUps").GetComponent<PowerUps>();

        //Fire Ball Effect
        fireBallEffect = GameObject.Find("FireBallEffect").GetComponent<ParticleSystem>();

        //Wall Explosion
        wallExplosion = GameObject.Find("WallExplosion").GetComponent<ParticleSystem>();

        // Setting reference to all safespots of level3 from here
        sp1level3 = GameObject.Find("SafeSpot1M3");
        sp2level3 = GameObject.Find("SafeSpot2M3");
        sp3level3 = GameObject.Find("SafeSpot3M3");
        sp4level3 = GameObject.Find("SafeSpot4M3");
        sp5level3 = GameObject.Find("SafeSpot5M3");


    }

    // Update is called once per frame
    void Update()
    {
        

        // Activating Taken FireBall

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(fireballCount > 0)
            {
                Debug.Log("Fire Ball Activated");
                fireBallEffect.Play();
                fireballTaken = true;
            }
        }

        // Wall Explosion
        if (wallExplosionOn)
        {
            coolDown -= Time.deltaTime;
        }
        if(coolDown < 0)
        {
            wallExplosion.Stop();
            wallExplosionOn = false;
            coolDown = 3;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Level 1 Collision
        if (collision.gameObject.CompareTag("sp1Level1"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            Destroy(sp1Level1);

        }
        if (collision.gameObject.CompareTag("sp2Level1"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            Destroy(sp2Level1);
        }
        if (collision.gameObject.CompareTag("sp3Level1"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            Destroy(sp3Level1);
        }
        if (collision.gameObject.CompareTag("sp4Level1"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            Destroy(sp4Level1);
        }

        // Level3 Collision
        if (collision.gameObject.CompareTag("SafeSpot1"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot1.SetActive(false);

        }
        if (collision.gameObject.CompareTag("SafeSpot2"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot2.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SafeSpot3"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot3.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SafeSpot4"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot4.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SafeSpot5"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot5.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SafeSpot6"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot6.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SafeSpot7"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot7.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SafeSpot8"))
        {
            safePosition = gameObject.transform.position;
            Debug.Log("Safe Spot Saved");
            spot8.SetActive(false);
        }

        // Wall Collosion Manager
        
       if (collision.gameObject.CompareTag("Wall"))
       {
            // If Fire Ball Taken Destroying the Wall
            if (fireballTaken)
            {
                wallExplosion.Play(); 
                wallExplosionOn = true;
                Destroy(collision.gameObject);
                fireBallEffect.Stop();

                fireballTaken = false;
                fireballCount--;
                Debug.Log("Fire Ball Remaining:- " + fireballCount);
            }
            else
            {
                setflags.moveForward = false;
                setflags.moveBackward = false;
                setflags.moveLeft = false;
                setflags.moveRight = false;

                player.transform.position = safePosition;
            }
        }
       
       // For Avoiding The Destruction of Outer Wall of Maze
        if (collision.gameObject.CompareTag("OuterWall"))
        {
            if (fireballTaken)
            {
                Debug.Log("Outer Wall Cannot Be Destroyed");
            }

            setflags.moveForward = false;
            setflags.moveBackward = false;
            setflags.moveLeft = false;
            setflags.moveRight = false;

            player.transform.position = safePosition;
        }

       // Checking if fire ball Taken
        if (collision.gameObject.CompareTag("FireBall1"))
        {
            fireballCount++;
            Destroy(fireball1);
            Debug.Log("Fire Ball Taken");
            Debug.Log("Fire Ball Current Count:- " + fireballCount);
        }
        if (collision.gameObject.CompareTag("FireBall2"))
        {
            fireballCount++;
            Destroy(fireball2);
            Debug.Log("Fire Ball Taken");
            Debug.Log("Fire Ball Current Count:- " + fireballCount);
        }


        // Giving access to player having this script to change safespot state
        if (gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Sp1Level3"))
            {
                safePosition = gameObject.transform.position;
                Debug.Log("Safe Spot Saved");
                 sp1level3.SetActive(false);

            }
            if (collision.gameObject.CompareTag("Sp2Level3"))
            {
                safePosition = gameObject.transform.position;
                Debug.Log("Safe Spot Saved");
                sp2level3.SetActive(false);

            }
            if (collision.gameObject.CompareTag("Sp3Level3"))
            {
                safePosition = gameObject.transform.position;
                Debug.Log("Safe Spot Saved");
                sp3level3.SetActive(false);

            }
            if (collision.gameObject.CompareTag("Sp4Level3"))
            {
                safePosition = gameObject.transform.position;
                Debug.Log("Safe Spot Saved");
                sp4level3.SetActive(false);

            }
            if (collision.gameObject.CompareTag("Sp5Level3"))
            {
                safePosition = gameObject.transform.position;
                Debug.Log("Safe Spot Saved");
                sp5level3.SetActive(false);

            }
        }




    }

}
