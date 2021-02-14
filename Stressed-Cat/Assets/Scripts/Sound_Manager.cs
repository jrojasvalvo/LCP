using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{

    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource[] sound = GetComponents<AudioSource>();
        backgroundMusic = sound[0];
        backgroundMusic.Stop();
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
