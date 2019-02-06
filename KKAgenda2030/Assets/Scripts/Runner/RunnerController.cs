using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour {

    public float upSpeed;
    public float downSpeed;

    public float speedFactor;
    public float maxSpeed;

    public float deceleration;
    public float acceleration;

    public LayerMask background;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        /*
        Programmed rigidbody vectors 
        */

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = (new Vector3(0, 0, upSpeed) + new Vector3(0, 0, -downSpeed)) * speedFactor;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, background) && Input.GetKey(KeyCode.Mouse0)) {
            speedFactor += Time.deltaTime * acceleration;
            speedFactor = Mathf.Clamp(speedFactor, 0f, maxSpeed);

            if (hit.point.z > transform.position.z) {
                rb.velocity = dir;
                upSpeed += Time.deltaTime * acceleration;
                upSpeed = Mathf.Clamp01(upSpeed);
            } else {
                upSpeed -= Time.deltaTime * deceleration;
                upSpeed = Mathf.Clamp01(upSpeed);
            }

            if (hit.point.z < transform.position.z) {
                rb.velocity = dir;
                downSpeed += Time.deltaTime * acceleration;
                downSpeed = Mathf.Clamp01(downSpeed);
            } else {
                downSpeed -= Time.deltaTime * deceleration;
                downSpeed = Mathf.Clamp01(downSpeed);
            }

        } else {
            speedFactor -= Time.deltaTime * deceleration;
            speedFactor = Mathf.Clamp(speedFactor, 0f, maxSpeed);

            upSpeed -= Time.deltaTime * deceleration;
            upSpeed = Mathf.Clamp01(upSpeed);

            downSpeed -= Time.deltaTime * deceleration;
            downSpeed = Mathf.Clamp01(downSpeed);

            rb.velocity = dir;
        }


        /*
        Unity Rigidbody acceleration based movement 
        */ 


        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 dir = transform.forward * speedFactor;

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, background) && Input.GetKey(KeyCode.Mouse0)) {
        //    speedFactor += Time.deltaTime * acceleration;
        //    speedFactor = Mathf.Clamp(speedFactor, 0f, maxSpeed);

        //    if (hit.point.z > transform.position.z) {
        //        rb.AddForce(dir, ForceMode.Acceleration);
        //    } else if (hit.point.z < transform.position.z) {
        //        rb.AddForce(-dir, ForceMode.Acceleration);
        //    }

        //} else {
        //    speedFactor -= Time.deltaTime * deceleration;
        //    speedFactor = Mathf.Clamp(speedFactor, 0f, maxSpeed);

        //    if (rb.velocity.z > 0) {
        //        rb.AddForce(dir, ForceMode.Acceleration);
        //    } else if (rb.velocity.z < 0) {
        //        rb.AddForce(-dir, ForceMode.Acceleration);
        //    }
        //}






        //Vector3 camPos = transform.position;
        //camPos.y += 10f;
        //Camera.main.transform.position = camPos;
    }
}


