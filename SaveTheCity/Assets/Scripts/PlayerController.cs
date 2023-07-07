using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 20.0f;
    public float runningSpeed = 40.0f;
    public float turnSpeed = 10.0f;
    public float gravityModifier = 1.5f;

    public float jumpSpeed = 50;
    public float turnspeed = 20;
    bool isOnGround = true;

    private Rigidbody playerRB;

   

    public bool moveForward = true;
    public bool moveBackward = true;
    public bool moveLeft = true;
    public bool moveRight = true;

    // Effect for High jump       JumpUps Not working perfectly fine
    public float effectcooldown = 2;
    private ParticleSystem powerUpsTaken;
    private bool jumpTaken = false;

    //  Contolling Animation

    public Animator playerAnimation;

    // Additional powerUp control
    public bool motionrestricted = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        powerUpsTaken = GameObject.Find("PowerTaken").GetComponent<ParticleSystem>();

        playerAnimation = GameObject.Find("Player").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        playerAnimation.SetFloat("runningspeed", walkingSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            if (jumpSpeed >= 1100)
            {
                powerUpsTaken.Play(); // play high jump Effect
                jumpTaken = true;
                effectcooldown = 2;
            }

            playerRB.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isOnGround = false;

            
        }

        if (jumpTaken)
        {
            effectcooldown -= Time.deltaTime;
        }

        if (effectcooldown <= 0)
        {
            powerUpsTaken.Stop(); // Stopping the effect
            jumpTaken = false;
        }



    }

    private void FixedUpdate()
    {
            MotionManager();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

    }


    void MoveForward()
    {
        if (moveForward && !motionrestricted)
        {
           float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput > 0)
            {
                playerAnimation.SetFloat("Speed_f", verticalInput);
                transform.Translate(Vector3.forward * Time.deltaTime * walkingSpeed * verticalInput);
            }
        }
    }

    void MoveBackward()
    {
        if (moveBackward && !motionrestricted)
        {
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput < 0)
                transform.Translate(Vector3.forward * Time.deltaTime * walkingSpeed * verticalInput);
        }
    }

    void MoveLeft()
    {
        if (moveLeft)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput < 0)
                //transform.Translate(Vector3.right * Time.deltaTime * walkingSpeed * horizontalInput);
                transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * turnspeed);
        }
    }

    void MoveRight()
    {
        if (moveRight)
        { 
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput > 0)
               // transform.Translate(Vector3.right * Time.deltaTime * walkingSpeed * horizontalInput);
                transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * turnspeed);
        }
    }


    void MotionManager()
    {
        MoveForward();

        if (!moveForward)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                moveForward = true;
                moveBackward = true;
            }
        }
        MoveBackward();

        if (!moveBackward)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveBackward = true;
                moveForward = true;
            }
        }

        MoveRight();

        if (!moveRight)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                moveRight = true;
                moveLeft = true;
            }
        }

        MoveLeft();

        if (!moveLeft)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveLeft = true;
                moveRight = true;
            }
        }
    }


    

   

    

    


}

