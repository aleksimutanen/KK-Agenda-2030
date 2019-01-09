using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMover : MonoBehaviour {

    Vector3 originalPos;

	void Start () {
        originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = originalPos + /*Vector3.right * Mathf.Sin(Time.time * Mathf.PI) * 0.05f*/
          /*  +*/ Vector3.forward * Mathf.Sin(Time.time * Mathf.PI + 0.5f * Mathf.PI) * 0.05f;
    }
}
