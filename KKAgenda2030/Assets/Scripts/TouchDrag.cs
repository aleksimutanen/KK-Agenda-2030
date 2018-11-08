using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : MonoBehaviour {



	// Use this for initialization
	void Start () {
        Touch touch = Input.GetTouch(0);
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        FirstTouch();
	}

    void FirstTouch()
    {
        Vector3 pos;

        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, transform.position.z);
        transform.position = pos;

    }
}
