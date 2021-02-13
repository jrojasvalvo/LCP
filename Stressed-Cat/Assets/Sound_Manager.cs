using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();

        backgroundMusic.Stop();
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
