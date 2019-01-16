using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloater : MonoBehaviour {

    Vector3 originalPos;
    float s;
    float f;

	void Start () {
        originalPos = transform.position;
        s = Random.Range(0.001f, 0.1f);
        f = Random.Range(0.2f, 0.7f);
	}

    void Update() {
        transform.position = originalPos + /*Vector3.right * Mathf.Sin(Time.time * Mathf.PI) * 0.05f*/
                                           /*  +*/ Vector3.forward * Mathf.Sin(Time.time * Mathf.PI + f * Mathf.PI) * s;
    }
}
