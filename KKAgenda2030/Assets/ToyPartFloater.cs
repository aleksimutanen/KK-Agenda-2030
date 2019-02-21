using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPartFloater : MonoBehaviour
{
    Vector3 originalPos;
    float s;
    float f;

    void Start() {
        originalPos = transform.position;
        s = Random.Range(1f, 2f);
        f = Random.Range(2f, 3f);
    }

    void Update() {
        transform.position = originalPos + /*Vector3.right * Mathf.Sin(Time.time * Mathf.PI) * 0.05f*/
                                           /*  +*/ Vector3.down * Mathf.Sin(Time.time * Mathf.PI + f * Mathf.PI) * s;
    }
}
