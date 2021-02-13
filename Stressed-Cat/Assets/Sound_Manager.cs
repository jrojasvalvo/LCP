using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource jumpSound;
    public AudioSource climbSound;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sound = GetComponents<AudioSource>();
        backgroundMusic = sound[0];
        backgroundMusic.Stop();
        backgroundMusic.Play();
        jumpSound = sound[1];
        climbSound = sound[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJump()
    {
        jumpSound.Stop();
        jumpSound.Play();
    }

    public void PlayClimb()
    {
        climbSound.Stop();
        climbSound.Play();
    }
}
