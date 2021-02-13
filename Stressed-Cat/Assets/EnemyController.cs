using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private float direction;
    private bool facingRight;
    public float rightBoundary;
    public float leftBoundary;

    //Line of Sight variables
    public float angle;
    public float radius;
    private GameObject Player;

    void Start()
    {
        facingRight = true;
        direction = 1;
        Player = GameObject.Find("Cat");
    }

    void FixedUpdate()
    {
        //Movement
        Vector2 movement = new Vector2(speed * direction, 0);
        transform.Translate(movement * Time.deltaTime);
        if (facingRight && transform.position.x > rightBoundary)
        {
            facingRight = false;
            direction = -1;

        }
        if (!facingRight && transform.position.x < leftBoundary)
        {
            facingRight = true;
            direction = 1;

        }

        //Line of Sight
        Vector2 player_pos = Player.transform.position;
        Vector2 forward = new Vector2(direction, 0);
        Vector2 v = player_pos - (Vector2)transform.position;
        if (Mathf.Abs(Vector2.Angle(forward, v)) < angle && Vector2.Distance(player_pos, (Vector2)transform.position) < radius)
        {
            Debug.Log("uwu hello there");
        }
    }

    void LineOfSight()
    {

    }
}
