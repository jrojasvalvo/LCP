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
    

    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //Jumping
        if (Input.GetButton("Jump"))
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
    //Check if Grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") {
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") {
            grounded = false;
        }
    }
}