﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public float tfSpeed;
    public float rbSpeed;

    public List<Transform> parallaxLayers;
    public List<float> parallaxFactors;


    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        tfSpeed += Time.deltaTime * 0.01f;
        var deltaPos = -transform.right * tfSpeed * Time.deltaTime;
        transform.position += deltaPos;
        for (int i = 0; i < parallaxLayers.Count; i++) {
            parallaxLayers[i].position += deltaPos * parallaxFactors[i];
        }

        //rbSpeed += Time.deltaTime * 1f;
        //rb.velocity = -transform.right * rbSpeed * Time.deltaTime;
    }

    //void FixedUpdate() {
        //rbSpeed += Time.deltaTime * 1f;
        //rb.velocity = -transform.right * rbSpeed * Time.deltaTime;
    //}
}
