using UnityEngine;
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

    public float initial_x = -11f;
    public float initial_y = -3.5f;

    public bool dead;

    private AudioSource jumpSound;
    private AudioSource climbSound;

    bool playJumpSound;
    
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = true;
        climb = false;
        facingRight = true;
        transform.position = new Vector3(initial_x, initial_y, 0);
        dead = false;

        AudioSource[] sound = GetComponents<AudioSource>();

        jumpSound = sound[0];
        climbSound = sound[1];

        playJumpSound = false;
    }


    void Update()
    {
        //Jumping and Climbing
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                fastFall = 0f;

                playJumpSound = true;
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
        Debug.Log(rb.velocity.y); 

        if(dead) {
            transform.position = new Vector3(initial_x, initial_y, 0);
            grounded = true;
            climb = false;
            this.gameObject.GetComponent<Stress_System>().stress_level = 0;
            dead = false;
        }

        if (playJumpSound == true)
        {
            jumpSound.Stop();
            jumpSound.Play();
            playJumpSound = false;
        }

    }

    void FixedUpdate()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    //Check if Grounded
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Ladder") grounded = true;
        if (col.gameObject.tag == "Ladder") 
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.gravityScale = 0.0f;
            climb = true;
            fastFall = 0f;
            grounded = true;

            climbSound.Stop();
            climbSound.Play();
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
            climbSound.Stop();
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