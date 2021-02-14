using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    private AudioSource backgroundMusic;
    private AudioSource WinMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sound = GetComponents<AudioSource>();
        backgroundMusic = sound[0];
        WinMusic = sound[1];
        backgroundMusic.Stop();
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWinMusic()
    {
        backgroundMusic.Stop();
        WinMusic.Stop();
        WinMusic.Play();
    }
}
