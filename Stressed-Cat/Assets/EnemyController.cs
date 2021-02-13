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

    void Start()
    {
        facingRight = true;
        direction = 1;
    }

    void FixedUpdate()
    {
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
    }

    void LineOfSight()
    {

    }
}
