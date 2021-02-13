using System;
using UnityEngine;

public class Stress_System : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_stress = 100f;
    public float stress_level = 0f;
    private bool startled = false;
    public GameObject enemies;
    public GameObject donuts;
    private Transform[] all_enemies;
    private Transform[] all_donuts;

    void Start()
    {
        startled = false;
        all_enemies = enemies.GetComponentsInChildren<Transform>();
        all_donuts = donuts.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        float player_x = transform.position.x;
        float player_y = transform.position.y;

        //Stress Proximity Variables:
        float enemy_x;
        float enemy_y;
        
        //Donut Proximity Variables:
        float donut_x;
        float donut_y;

        float x_diff;
        float y_diff;
        float dist;


        if(stress_level >= max_stress) {
            startled = true;
        }
        //Check for proximity to enemies to increase stress level:
        for(int i = 0; i < all_enemies.Length; i++) {
            enemy_x = all_enemies[i].position.x;
            enemy_y = all_enemies[i].position.y;
            x_diff = Math.Abs(player_x - enemy_x);
            y_diff = Math.Abs(player_y - enemy_y);
            dist = (float)(Math.Sqrt(x_diff * x_diff + y_diff * y_diff));
            if(dist <= 5) {
                stress_level += (5 - dist) / 100;
            }
        }
        //Check if the player is close enough to any donuts to collect them:
        for(int i = 0; i < all_donuts.Length; i++) {
            donut_x = all_donuts[i].position.x;
            donut_y = all_donuts[i].position.y;
            x_diff = Math.Abs(player_x - donut_x);
            y_diff = Math.Abs(player_y - donut_y);
            dist = (float)(Math.Sqrt(x_diff * x_diff + y_diff * y_diff));
            if(dist <= 0.6 && all_donuts[i].gameObject.active) {
                all_donuts[i].gameObject.SetActive(false);
                stress_level -= 20;
            }
        }
        print(stress_level);
    }
}
