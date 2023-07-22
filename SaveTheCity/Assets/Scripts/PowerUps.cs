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

    public int maxJumpUps = 0;
    public int maxSpeedUps = 0;

    private float effectcooldown = 1;

    // PowerUps Taken Particle Effect
    private ParticleSystem powerUpsTaken;

    float countDown = 10;  // Power Upgrade For 10 sec
    
    // Maintaining Ui changes
    private InGameUI gameUI;

    // Start is called before the first frame update
    void Start()
    {
        upgrades = GameObject.Find("Player").GetComponent<PlayerController>();
        gameUI = GameObject.Find("UIManager").GetComponent<InGameUI>();

        powerUpsTaken = GameObject.Find("PowerTaken").GetComponent<ParticleSystem>();  // Getting Access To Particle Sysytem
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
    }

   

    public void GetSpeedUp()
    {
        if (maxSpeedUps > 0)
        {
            Debug.Log("Speed Upgraded");
            upgrades.walkingSpeed = 80;
            maxSpeedUps--;

            // Jump Up Effect
            powerUpsTaken.Play();
            StartCoroutine(StopEffect());       // Stopping the effect 

            StartCoroutine(StopSpeedUp());     // Deactivate Ability

        }
    }

    public void GetJumpUp()
    {
        if (maxJumpUps > 0)
        {
            Debug.Log("Jump Upgraded");
            upgrades.jumpSpeed = 1300;
            maxJumpUps--;

            // Jump Up Effect
            powerUpsTaken.Play();
            StartCoroutine(StopEffect());       // Stopping the effect 

            StartCoroutine(StopJumpUp());    // Deactivate The Ability
        }
    }

    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(countDown);

        Debug.Log("Speed Set To Normal");
        effectcooldown = 2;    // Setting up Effect Cooldown for next Effect
        upgrades.walkingSpeed = 30.0f;
    }

    IEnumerator StopJumpUp()
    {
        yield return new WaitForSeconds(countDown);

        Debug.Log("Jump Set To Normal");
        upgrades.jumpSpeed = 1000;
    }

    IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(effectcooldown);
        powerUpsTaken.Stop();
    }


    private void OnCollisionEnter(Collision collision)
    {
        // SpeedUps Collision Control
        if (collision.gameObject.CompareTag("SpeedUp"))
        {
            gameUI.SpeedUpTakenUpdate();    // Set UI Update
            maxSpeedUps++;
            Destroy(collision.gameObject);
            Debug.Log("SpeedsUps Remaining:- "+ maxSpeedUps);
        }
        
        // JumpUps Collision Control
        if (collision.gameObject.CompareTag("JumpUp"))
        {
            gameUI.JumpUpTakenUpdate();   // Set UI Updates
            maxJumpUps++;
            Destroy(collision.gameObject);
            Debug.Log("JumpUps Remaining:- " + maxJumpUps);
        }
}




}
