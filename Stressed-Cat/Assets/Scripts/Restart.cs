using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject cam;
    public GameObject player;

    public IEnumerator restart() {
        cam.GetComponent<ScreenShake>().shake();
        player.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
