using System;
using UnityEngine;

public class Stress_System : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_stress = 100f;
    public float stress_level = 0f;
    private bool startled = false;
    public GameObject enemies;
    private Transform[] all_enemies;

    void Start()
    {
        startled = false;
        all_enemies = enemies.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   
        float player_x = transform.position.x;
        float player_y = transform.position.y;
        float enemy_x;
        float enemy_y;
        float x_diff;
        float y_diff;
        float dist;

        if(stress_level >= max_stress) {
            startled = true;
        }
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
        print(stress_level);
    }
}
