using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    private AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        buttonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNext()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            /*string currScene = SceneManager.GetActiveScene().name.Substring(5);
            if (currScene == "") {
                next = "Main Menu";
            } else {
                int currSceneNum = Int32.Parse(currScene);
                string nextSceneNum = (currSceneNum + 1).ToString();

                next = "Level" + nextSceneNum;
            }
            SceneManager.LoadScene(next);*/
        }
    }

    void OnMouseDown()
    {
        LoadNext();
        //buttonSound.Play();
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 0, 255, 255);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }
}
