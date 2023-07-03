using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    // No powerUps in Level1 , SpeedUps and jumpUps in level2, SpeedUps JumpUps and FireBall in level3
    // 
    // Let Them Earn Power PowerUps

    // When Collide with power We Earn Power if Power Earned Then we can use them with 'G','H'.

   private PlayerController upgrades;

    public GameObject speed1;
    public GameObject speed2;
    public GameObject speed3;
    public GameObject speed4;

    public GameObject jump1;
    public GameObject jump2;
    public GameObject jump3;
    public GameObject jump4;

    int maxJumpUps = 0;
    int maxSpeedUps = 0;

    bool jumpTaken = false;
    bool speedTaken = false;
    bool powerUpTimer = false;

    private float effectcooldown = 2;

    // PowerUps Taken Particle Effect
    private ParticleSystem powerUpsTaken;

    float countDown = 10;  // Power Upgrade For 10 sec


    // Start is called before the first frame update
    void Start()
    {
        upgrades = GameObject.Find("Player").GetComponent<PlayerController>();
        // Getting Access To Particle Sysytem
        powerUpsTaken = GameObject.Find("PowerTaken").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.G))
        {
                GetSpeedUp();

        } 
        if (Input.GetKeyDown(KeyCode.J))
        {
                GetJumpUp();
        }

        // Start The CountDown
        if (powerUpTimer) {
            countDown -= Time.deltaTime;
            effectcooldown -= Time.deltaTime;
        }

        // When Timer Over Set Things Back To Normal
        if(countDown < 0 && powerUpTimer) {

            if (speedTaken){
                Debug.Log("Speed Set To Normal");
                effectcooldown = 2;    // Setting up Effect Cooldown for next Effect
                upgrades.walkingSpeed = 30.0f;
                countDown = 10;
                speedTaken = false;
            }
            if (jumpTaken){
                Debug.Log("Jump Set To Normal");
                upgrades.jumpSpeed = 1000;
                effectcooldown = 2; // Setting up Effect Cooldown for next Effect
                countDown = 10;
                jumpTaken = false;
            }
            powerUpTimer = false;
        }


        if (effectcooldown <= 0)
        {
            powerUpsTaken.Stop(); // Stopping the effect
        }


    }

   

    public void GetSpeedUp()
    {
        if (!(maxSpeedUps <= 0))
        {
            Debug.Log("Speed Upgraded");
            powerUpsTaken.Play();
            upgrades.walkingSpeed = 80;
            powerUpTimer = true;
            speedTaken = true;
            maxSpeedUps--;
        }
    }
    public void GetJumpUp()
    {
        if (!(maxJumpUps <= 0))
        {
            Debug.Log("Jump Upgraded");
            powerUpsTaken.Play();
            upgrades.jumpSpeed = 1300;
            powerUpTimer = true;
            jumpTaken = true;
            maxJumpUps--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        // SpeedUps Collision Control
        if (collision.gameObject.CompareTag("Speed1"))
        {
            maxSpeedUps++;
            Destroy(speed1);

            Debug.Log("SpeedsUps Remaining:- "+ maxSpeedUps);

        }
        if (collision.gameObject.CompareTag("Speed3"))
        {
            maxSpeedUps++;
            Destroy(speed3);
            Debug.Log("SpeedsUps Remaining:- " + maxSpeedUps);
        }
        if (collision.gameObject.CompareTag("Speed2"))
        {
            maxSpeedUps++;
            Destroy(speed2);
            Debug.Log("SpeedsUps Remaining:- " + maxSpeedUps);
        }
        if (collision.gameObject.CompareTag("Speed4"))
        {
            maxSpeedUps++;
            Destroy(speed4);
            Debug.Log("SpeedsUps Remaining:- " + maxSpeedUps);
        }

        // JumpUps Collision Control
        if (collision.gameObject.CompareTag("Jump1"))
        {
            maxJumpUps++;
            Destroy(jump1);

            Debug.Log("JumpUps Remaining:- " + maxJumpUps);

        }
        if (collision.gameObject.CompareTag("Jump3"))
        {
            maxJumpUps++;
            Destroy(jump3);
            Debug.Log("JumpUps Remaining:- " + maxJumpUps);
        }
        if (collision.gameObject.CompareTag("Jump2"))
        {
            maxJumpUps++;
            Destroy(jump2);
            Debug.Log("JumpUps Remaining:- " + maxJumpUps);
        }
        if (collision.gameObject.CompareTag("Jump4"))
        {
            maxJumpUps++;
            Destroy(jump4);
            Debug.Log("JumpUps Remaining:- " + maxJumpUps);
        }

        

}




}
