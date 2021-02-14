using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0f;
    public float magnitude = 0.2f;
    public float dampingSpeed = 1.0f;
    private Vector3 initialPosition;
    public GameObject player;
    private Vector3 player_init;
    public Vector3 offset = new Vector3(-5, -2, 0);

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
        player = GameObject.FindGameObjectWithTag("Player");
        player_init = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * magnitude;

            duration -= Time.fixedDeltaTime * dampingSpeed;
        }
        else
        {
            duration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    void LateUpdate()
    {
        Vector3 pos = player.transform.position;  // Get new player position
        //Camera.main.transform.localPosition = initialPosition + pos - player_init; //Use offset
        
        //makes camera offset consistent bc it doesn't rely on starting position
        //Camera.main.transform.localPosition = pos - offset; //Use offset

        //centered
        Camera.main.transform.position = new Vector3(pos.x, pos.y, -5);
    }

    public void shake()
    {
        duration = 0.2f;
    }
}
