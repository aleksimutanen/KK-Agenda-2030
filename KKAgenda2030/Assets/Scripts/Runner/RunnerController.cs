using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour {

    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;

    [SerializeField] float speedFactor;
    [SerializeField] float maxSpeed;

    [SerializeField] float deceleration;
    [SerializeField] float acceleration;

    [SerializeField] float distanceThreshold;

    [SerializeField] LayerMask background;

    [HideInInspector] public bool cameraMove = true;
    [HideInInspector] public Vector3 deltaPos;
    Rigidbody rb;
    Vector3 charStartPos;

    float tfSpeed = 1.5f;

    void Start() {
        charStartPos = transform.position;
        rb = GetComponent<Rigidbody>();
        cameraMove = true;
    }

    void FixedUpdate() {
        /*
        Programmed rigidbody vectors 
        */

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = (new Vector3(0, 0, upSpeed) + new Vector3(0, 0, -downSpeed)) * speedFactor;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, background) && Input.GetKey(KeyCode.Mouse0)) {
            speedFactor += Time.deltaTime * acceleration;
            speedFactor = Mathf.Clamp(speedFactor, 0f, maxSpeed);

            if (hit.point.z > transform.position.z && Vector3.Distance(new Vector3(0,0,hit.point.z), new Vector3(0,0,transform.position.z)) > distanceThreshold) {
                upSpeed += Time.deltaTime * acceleration;
                upSpeed = Mathf.Clamp01(upSpeed);
                rb.velocity = dir;
            } else {
                upSpeed -= Time.deltaTime * deceleration;
                upSpeed = Mathf.Clamp01(upSpeed);
            }

            if (hit.point.z < transform.position.z && Vector3.Distance(new Vector3(0, 0, hit.point.z), new Vector3(0, 0, transform.position.z)) > distanceThreshold) {
                downSpeed += Time.deltaTime * acceleration;
                downSpeed = Mathf.Clamp01(downSpeed);
                rb.velocity = dir;
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





        //ignore

        tfSpeed += Time.deltaTime * 0.01f;
        deltaPos = transform.right * tfSpeed * Time.deltaTime;
        transform.position += deltaPos;

        if (/*bool jtn*/cameraMove) {
            Vector3 camPos = transform.position;
            camPos.x += 5f;
            camPos.y += 10f;
            camPos.z = 0;
            Camera.main.transform.position = camPos;
        }
    }

    public void ResetCharacter() {
        rb.velocity = Vector3.zero;
        transform.position = charStartPos;
        speedFactor = 0f;
    }

}


