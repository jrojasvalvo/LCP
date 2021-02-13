using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
<<<<<<< Updated upstream
    private AudioSource backgroundMusic;
=======
    private AudioSource backgroundmusic;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        backgroundMusic = GetComponent<AudioSource>();

        backgroundMusic.Stop();
        backgroundMusic.Play();
=======
        backgroundmusic = GetComponent<AudioSource>();

        backgroundmusic.Stop();
        backgroundmusic.Play();
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
