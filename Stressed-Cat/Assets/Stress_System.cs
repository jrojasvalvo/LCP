using System;
using UnityEngine;
using UnityEngine.UI;

public class Stress_System : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_stress = 100f;
    public float stress_level = 0f;
    private bool startled;
    public bool meditating = false;

    public GameObject enemies;
    public Image stress_bar;
    

    private Transform[] all_enemies;
    public float distance_from_hostile = 5f;

    public float donut_stress_amt = 20f;
    public float donut_pickup_distance = 0.6f;

    private float meditation_start;
    public int meditation_time = 5;
    public bool canMeditate = true;
    public float enemy_stress_rate;
    public bool inWater = false;
    public float water_stress_amt = 0.1f;
    public float timeSlowRate = 0.05f;
    bool slowDown = false;
    

    public Image meditationBar;

    private AudioSource donutSound;
    private AudioSource catScaredSound;
    private AudioSource meditationSound;
    private AudioSource catJump;
    private AudioSource catClimb;
    private AudioSource dogBark;

    void Start()
    {
        meditationBar.fillAmount = 0;
        startled = false;
        all_enemies = enemies.GetComponentsInChildren<Transform>();
        stress_bar.fillAmount = 1;

        AudioSource[] sound = GetComponents<AudioSource>();
        donutSound = sound[0];
        catScaredSound = sound[1];
        meditationSound = sound[2];
        catJump = sound[3];
        catClimb = sound[4];
        dogBark = sound[5];

    }

    // Update is called once per frame
    void Update()
    {   
        stress_bar.fillAmount = 1 - stress_level / max_stress;
        stress_bar.color = new Color32((byte)(stress_level),
                                       (byte)(100 - stress_level),0,255);

        if(Input.GetKey(KeyCode.E) && canMeditate) {
            canMeditate = false;
            meditating = true;
            meditation_start = Time.time;
            gameObject.GetComponent<PlayerController>().canMove = false;
            meditationSound.Stop();
            meditationSound.Play();
        }
        float player_x = transform.position.x;
        float player_y = transform.position.y;

        //Stress Proximity Variables:
        float enemy_x;
        float enemy_y;
        float x_diff;
        float y_diff;
        float dist;

        if(stress_level >= max_stress) {
            //startled = true;
            //Need this so the screen doesn't constantly shake
            gameObject.GetComponent<PlayerController>().dead = true;
            catScaredSound.Stop();
            catScaredSound.Play();

        }
        if(meditating) {
            meditationBar.fillAmount = (Time.time - meditation_start) / meditation_time;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if(Time.time - meditation_time >= meditation_start) {
                meditating = false;
                canMeditate = true;
                stress_level = 0f;
                gameObject.GetComponent<PlayerController>().canMove = true;
                meditationBar.GetComponent<Image>().fillAmount = 0;
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
                stress_level += (distance_from_hostile - dist) * enemy_stress_rate;
            }
        }

        if (inWater) {
            stress_level += water_stress_amt;
        }
        if (slowDown) {
            if (Time.timeScale - timeSlowRate < 0) {
                Time.timeScale = 0;
            } else {
                Time.timeScale -= timeSlowRate;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Water") {
            inWater = true;
        }
        if (collider.tag == "Donut") {
            collider.gameObject.SetActive(false);
            stress_level -= donut_stress_amt;
            donutSound.Stop();
            donutSound.Play();
            if(stress_level <= 0) {
                stress_level = 0;
            }
        }
        if (collider.tag == "End" && Time.timeScale > 0) {
            collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            slowDown = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Water") {
            inWater = false;
        }
    }

    public void PlayJump()
    {
        catJump.Stop();
        catJump.Play();
    }

    public void PlayClimb()
    {
        catClimb.Stop();
        catClimb.Play();
    }

    public void StopJump()
    {
        catJump.Stop();
    }
    public void StopClimb()
    {
        catClimb.Stop();
    }

    public void PlayBark()
    {
        dogBark.Stop();
        dogBark.Play();
        catScaredSound.Stop();
        catScaredSound.Play();
    }
}
