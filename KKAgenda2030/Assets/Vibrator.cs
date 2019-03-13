using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    public float amplitude;
    public float frequency;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.up * Mathf.Sin(2 * Mathf.PI / (1 / frequency) * Time.time) * amplitude;


    }
}
