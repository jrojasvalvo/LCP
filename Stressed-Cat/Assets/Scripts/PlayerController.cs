﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject cam;
<<<<<<< HEAD:Stressed-Cat/Assets/Scripts/PlayerController.cs
    public GameObject gameManager;
=======
>>>>>>> 7ff825da3c49b525341ef1a121cda648d6f5a1ae:Stressed-Cat/Assets/PlayerController.cs

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    private bool facingRight;

    //Grounded Vars
    bool grounded = true;
    float fastFall = 0f;
    public float fastFallSpeed;
    private bool climb;
    public bool canMove = true;

<<<<<<< HEAD:Stressed-Cat/Assets/Scripts/PlayerController.cs
    // public float initial_x = -8f;
    // public float initial_y = -3.5f;
    Vector3 initialPosition;
=======
    public float initial_x = -7.13f;
    public float initial_y = -2.5f;

    public Vector3 camera_init;

>>>>>>> 7ff825da3c49b525341ef1a121cda648d6f5a1ae:Stressed-Cat/Assets/PlayerController.cs
    public bool dead;
    public bool canJump = true;
    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = true;
        climb = false;
        facingRight = true;
        //transform.position = new Vector3(initial_x, initial_y, 0);
        dead = false;
<<<<<<< HEAD:Stressed-Cat/Assets/Scripts/PlayerController.cs
        initialPosition = transform.position;
=======
        cam = GameObject.Find("Main Camera");
        camera_init = cam.transform.position;
>>>>>>> 7ff825da3c49b525341ef1a121cda648d6f5a1ae:Stressed-Cat/Assets/PlayerController.cs
    }
    IEnumerator jumpAnim()
    {
        anim.SetTrigger("jump_start");
        yield return new WaitForSeconds(0.25f);
    }

    void Update()
    {   
        if (canMove) {
            //Jumping and Climbing
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
            {
                if (grounded && canJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump);
                    fastFall = 0f;
                    anim.SetTrigger("jump_start");
                }
            } else {
                if (!grounded){
                    fastFall = fastFallSpeed;
                }
            }
            //can't just hold down the jump button and keep jumping
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) 
                || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W)) {
                    canJump = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) 
                || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)) {
                    canJump = false;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (climb)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -jump);
                }
            }

            moveVelocity = 0;

            //Left Right Movement
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                moveVelocity = -speed;
                if (facingRight)
                {
                    reverseImage();
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                moveVelocity = speed;
                if (!facingRight)
                {
                    reverseImage();
                }
            }

            rb.velocity = new Vector2(moveVelocity, rb.velocity.y - fastFall);
        }
        if (Time.timeScale == 0) canMove = false;

        if(dead) {
<<<<<<< HEAD:Stressed-Cat/Assets/Scripts/PlayerController.cs
            //transform.position = new Vector3(initial_x, initial_y, 0);
            
            // transform.position = initialPosition;
            // grounded = true;
            // climb = false;
            // this.gameObject.GetComponent<Stress_System>().stress_level = 0;
            // dead = false;
            // canMove = true;
            // this.gameObject.GetComponent<Stress_System>().meditating = false;
            // this.gameObject.GetComponent<Stress_System>().meditationBar.fillAmount = 0;
            // this.gameObject.GetComponent<Stress_System>().canMeditate = true;
            // Time.timeScale = 1;
                
            // camera.GetComponent<ScreenShake>().shake();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Doing this because if we do the stuff above, it doesn't respawn the donuts
            gameManager.GetComponent<gameManager>().callRestart();
=======
            transform.position = new Vector3(initial_x, initial_y, 0);
            grounded = true;
            climb = false;
            this.gameObject.GetComponent<Stress_System>().stress_level = 0;
            dead = false;
            canMove = true;
            this.gameObject.GetComponent<Stress_System>().meditating = false;
            this.gameObject.GetComponent<Stress_System>().meditationBar.fillAmount = 0;
            this.gameObject.GetComponent<Stress_System>().canMeditate = true;

            cam.transform.position = camera_init;
            //cam.GetComponent<ScreenShake>().shake();
>>>>>>> 7ff825da3c49b525341ef1a121cda648d6f5a1ae:Stressed-Cat/Assets/PlayerController.cs
        }
        LoadNext();
    }

    void FixedUpdate()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if (grounded)
        {
            anim.SetTrigger("landing");
        }
        else
        {
            anim.ResetTrigger("landing");
        }
    }

    //Check if Grounded
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" )
        {
           grounded = true;
           anim.SetTrigger("landing");
        }
        
        if (col.gameObject.tag == "Ladder") grounded = true;

        if (col.gameObject.tag == "Ladder") 
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.gravityScale = 0.0f;
            climb = true;
            fastFall = 0f;
            grounded = true;
        }
        if (col.gameObject.tag == "Sight") {
            dead = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {   
        
        //if (col.gameObject.tag == "Ground") grounded = false;
        //replace with object(s) name
        if (col.gameObject.tag == "Ladder")
        {
            grounded = false;
            climb = false;
            rb.gravityScale = 1.0f;
        }
        if(col.gameObject.tag == "Ground") {
            grounded = false;
        }
    }

    //Check if on Climbable Object
    void OnTriggerStay2D(Collider2D col)
    {
        //replace with object(s) name
        if (col.gameObject.tag == "Ladder")
        {
            climb = true;
            grounded = true;
            rb.gravityScale = 0.0f;
            fastFall = 0f;
        }
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    void reverseImage()
    {
        facingRight = !facingRight;
        Vector2 scale = rb.transform.localScale;
        scale.x *= -1;
        rb.transform.localScale = scale;
        //do not flip camera
        Vector2 cscale = cam.transform.localScale;
        cscale.x *= -1;
        //cam.transform.localScale = cscale; 
    }

    //Go to next level
    string next;

    public void LoadNext() {
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
            string currScene = SceneManager.GetActiveScene().name.Substring(5);
            if (currScene == "") {
                next = "Main Menu";
            } else {
                int currSceneNum = Int32.Parse(currScene);
                string nextSceneNum = (currSceneNum + 1).ToString();

                next = "Level" + nextSceneNum;
            }
            SceneManager.LoadScene(next);
        }
    }
}