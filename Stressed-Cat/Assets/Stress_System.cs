using System;
using UnityEngine;

public class Stress_System : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_stress = 100f;
    public float stress_level = 0f;
    private bool startled;
    public bool meditating = false;

    public GameObject enemies;
    public GameObject donuts;
    public GameObject stress_bar;

    private Transform[] all_enemies;
    private Transform[] all_donuts;
    public float distance_from_hostile = 5f;

    public float donut_stress_amt = 20f;
    public float donut_pickup_distance = 0.6f;

    private float meditation_start;
    public int meditation_time = 5;
    private bool canMeditate = true;

    void Start()
    {
        startled = false;
        all_enemies = enemies.GetComponentsInChildren<Transform>();
        all_donuts = donuts.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        stress_bar.transform.localScale = new Vector3((1f - (stress_level / max_stress)), 
                                                       stress_bar.transform.localScale.y,
                                                       stress_bar.transform.localScale.z);

        if(Input.GetKey(KeyCode.E) && canMeditate) {
            canMeditate = false;
            meditating = true;
            meditation_start = Time.time;
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
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
        if(meditating) {
            print(Time.time);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if(Time.time - meditation_time >= meditation_start) {
                meditating = false;
                canMeditate = true;
                stress_level = 0f;
                gameObject.GetComponent<PlayerController>().enabled = true;
            }
        }
        //Check for proximity to enemies to increase stress level:
        for(int i = 0; i < all_enemies.Length; i++) {
            enemy_x = all_enemies[i].position.x;
            enemy_y = all_enemies[i].position.y;
            x_diff = Math.Abs(player_x - enemy_x);
            y_diff = Math.Abs(player_y - enemy_y);
            dist = (float)(Math.Sqrt(x_diff * x_diff + y_diff * y_diff));
            if(dist <= distance_from_hostile) {
                stress_level += (distance_from_hostile - dist) / 100f;
            }
        }
        //Check if the player is close enough to any donuts to collect them:
        for(int i = 0; i < all_donuts.Length; i++) {
            donut_x = all_donuts[i].position.x;
            donut_y = all_donuts[i].position.y;
            x_diff = Math.Abs(player_x - donut_x);
            y_diff = Math.Abs(player_y - donut_y);
            dist = (float)(Math.Sqrt(x_diff * x_diff + y_diff * y_diff));
            if(dist <= donut_pickup_distance && all_donuts[i].gameObject.active) {
                
                all_donuts[i].gameObject.SetActive(false);
                stress_level -= donut_stress_amt;
            }
        }
    }
}
