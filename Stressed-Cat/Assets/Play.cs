using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
     private AudioSource buttonSound;

    public Sound_Manager SoundManager;

    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        buttonSound.Stop();
        buttonSound.Play();
        SceneManager.LoadScene("Level1");
    }

    void OnMouseEnter() {
        GetComponent<SpriteRenderer>().color = new Color32(255, 0, 255, 255);
    }

    void OnMouseExit() {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }
}
