using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpotManager : MonoBehaviour
{
    public GameObject player;

    public ParticleSystem fireBallEffect;

    public Vector3 safePosition = new Vector3(0,0.9f,0);

    private PlayerController setflags;

    public bool collidedwithmazewall = false;
    public bool collidedwithouterwall = false;
    public bool safespotcollected = false;

    // Fire Ball Manager
    private bool fireballTaken = false;
    public int fireballCount = 0;

    private InGameUI gameUI;    // Fireball UI changes

    // Managing Wall Explosion
    private ParticleSystem wallExplosion;
    float coolDown = 3;

    private AudioSource playaudio;
    public AudioClip mazewallhit;
    public AudioClip outerwallhit;
    public AudioClip safespot;
    public AudioClip poweruptaken;
    public AudioClip fireballinuse;

    // Start is called before the first frame update
    void Start()
    {
        setflags = GameObject.Find("Player").GetComponent<PlayerController>();
        gameUI = GameObject.Find("UIManager").GetComponent<InGameUI>();
        playaudio = GameObject.Find("Player").GetComponent<AudioSource>();

        //Fire Ball Effect
        fireBallEffect = GameObject.Find("FireBallEffect").GetComponent<ParticleSystem>();

        //Wall Explosion
        wallExplosion = GameObject.Find("WallExplosion").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activating Taken FireBall

        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateFireBall();
        }
    }

    void ActivateFireBall()
    {
        if (fireballCount > 0)
        {
            Debug.Log("Fire Ball Activated");
            fireBallEffect.Play();
            fireballTaken = true;
        }
    }
    IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(coolDown);
        wallExplosion.Stop();
    }


    private void OnCollisionEnter(Collision collision)
    {
       // Wall Collosion Manager
       if (collision.gameObject.CompareTag("Wall"))
       {
            // If Fire Ball Taken Destroying the Wall
            if (fireballTaken)
            {
                playaudio.PlayOneShot(fireballinuse, 1);

                wallExplosion.Play();           // Start Wall Collision Effect 
                StartCoroutine(StopEffect());   // Stop effect of wall collision
                fireBallEffect.Stop();

                Destroy(collision.gameObject);
                fireballTaken = false;      // Deactivate Fireball when used
                fireballCount--;

                Debug.Log("Fire Ball Remaining:- " + fireballCount);
            }
            else
            {
                playaudio.PlayOneShot(mazewallhit, 1);

                collidedwithmazewall = true;

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
            collidedwithouterwall = true;

            setflags.moveForward = false;
            setflags.moveBackward = false;
            setflags.moveLeft = false;
            setflags.moveRight = false;

            playaudio.PlayOneShot(outerwallhit, 1);
            player.transform.position = safePosition;
        }

       // Checking if fire ball Taken
       if (collision.gameObject.CompareTag("FireBall"))
       {
            playaudio.PlayOneShot(poweruptaken, 1);

            gameUI.FireBallTakenUpdate();     // Set UI Update
            fireballCount++;
            Destroy(collision.gameObject);
            Debug.Log("Fire Ball Taken");
       }
       
            if (collision.gameObject.CompareTag("SafeSpot"))
            {
                playaudio.PlayOneShot(safespot, 1);
            
                safespotcollected = true;  // For UI Update
                safePosition = gameObject.transform.position;   // Save Player Position when SafeSpot Taken
                Debug.Log("Safe Spot Saved");
                Destroy(collision.gameObject);
            }
        
    }

}
