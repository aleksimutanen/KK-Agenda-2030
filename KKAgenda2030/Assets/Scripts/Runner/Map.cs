using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public float tfSpeed;
    public float rbSpeed;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        tfSpeed += Time.deltaTime * 0.01f;
        transform.position += -transform.right * tfSpeed * Time.deltaTime;

        //rbSpeed += Time.deltaTime * 1f;
        //rb.velocity = -transform.right * rbSpeed * Time.deltaTime;
    }

    //void FixedUpdate() {
        //rbSpeed += Time.deltaTime * 1f;
        //rb.velocity = -transform.right * rbSpeed * Time.deltaTime;
    //}
}
