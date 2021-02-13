using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    //Grounded Vars
    bool grounded = true;
    float fastFall = 0f;
    public float fastFallSpeed;
    private bool climb;
    public bool canMove = true;

    

    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        grounded = true;
        climb = false;
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
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                moveVelocity = speed;
            }

            rb.velocity = new Vector2(moveVelocity, rb.velocity.y - fastFall);
        }
        if (Time.timeScale == 0) canMove = false;

    }
    //Check if Grounded
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Ladder" || col.gameObject.tag == "Hostile") grounded = true;
        if (col.gameObject.tag == "Ladder") rb.velocity = new Vector2(rb.velocity.x,0);
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
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Hostile" || col.gameObject.tag == "Water") {
            grounded = false;
        }
    }

    //Check if on Climbable Object
    void OnTriggerStay2D(Collider2D col)
    {
        grounded = true;
        //replace with object(s) name
        if (col.gameObject.tag == "Ladder")
        {
            climb = true;
            rb.gravityScale = 0.0f;
            fastFall = 0f;
        }
    }
}