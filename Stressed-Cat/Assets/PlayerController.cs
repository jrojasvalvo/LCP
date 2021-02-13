﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

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

    

    public float initial_x = -8f;
    public float initial_y = -3.5f;

    public bool dead;
    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = true;
        climb = false;
        facingRight = true;
        transform.position = new Vector3(initial_x, initial_y, 0);
        dead = false;
    }
    IEnumerator jumpAnim()
    {
        anim.SetTrigger("jump_start");
        yield return new WaitForSeconds(0.25f);
        //anim.ResetTrigger("jump_start");
    }

    void Update()
    {   
        if (canMove) {
            //Jumping and Climbing
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
            {
                if (grounded)
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
            transform.position = new Vector3(initial_x, initial_y, 0);
            grounded = true;
            climb = false;
            this.gameObject.GetComponent<Stress_System>().stress_level = 0;
            dead = false;
        }
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
    }
}