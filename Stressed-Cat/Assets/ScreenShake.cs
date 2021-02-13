using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0f;
    public float magnitude = 0.2f;
    public float dampingSpeed = 1.0f;
    private Vector3 initialPosition;


    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * magnitude;

            duration -= Time.fixedDeltaTime * dampingSpeed;
        }
        else
        {
            duration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    
    public void shake()
    {
        duration = 0.2f;
    }
}
