using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    //Movement
    public float speed;
    public float jump;
    private bool climb;
    float moveVelocity;

    //Grounded Vars
    bool grounded;
    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        grounded = true;
        climb = false;
    }


    void Update()
    {
        //Jumping and Climbing
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
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

        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

    }
    //Check if Grounded
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground") grounded = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground") grounded = false;
        //replace with object(s) name
        if (col.gameObject.tag == "Ladder")
        {
            climb = false;
            rb.gravityScale = 1.0f;
            rb.velocity = new Vector2(rb.velocity.x,0);
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
        }
    }
}