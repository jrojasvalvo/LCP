using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    byte color = 255;
    public Image logo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Time.time >= 5) {
            if(color > 0) {
                color -= 1;
            }
            logo.color = new Color32(255, 255, 255, color);
        }
    }
}
