using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour {

    [SerializeField] float tfSpeed = 1.5f;
    float tfSpeedOnStart;

    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;

    [SerializeField] float speedFactor;
    [SerializeField] float maxSpeed;

    [SerializeField] float deceleration;
    [SerializeField] float acceleration;

    [SerializeField] float distanceThreshold;

    [SerializeField] LayerMask background;

    [HideInInspector] public bool canMove;
    [HideInInspector] public bool gameActive;
    [HideInInspector] public bool hitWeb;
    [HideInInspector] public bool cameraMove;
    [HideInInspector] public Vector3 deltaPos;
    Rigidbody rb;
    Vector3 charStartPos;
    [SerializeField] GameObject goalPosition;

    public AudioSource playerAudio;

    void Start() {
        tfSpeedOnStart = tfSpeed;
        charStartPos = transform.position;
        rb = GetComponent<Rigidbody>();
        cameraMove = true;
        gameActive = true;
        canMove = true;
    }

    void FixedUpdate() {
        /*
        Programmed rigidbody vectors 
        */

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = (new Vector3(0, 0, upSpeed) + new Vector3(0, 0, -downSpeed)) * speedFactor;

        if (canMove) {

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, background) && Input.GetKey(KeyCode.Mouse0)) {
                speedFactor += Time.deltaTime * acceleration;
                speedFactor = Mathf.Clamp(speedFactor, 0f, maxSpeed);

                if (hit.point.z > transform.position.z && Vector3.Distance(new Vector3(0, 0, hit.point.z), new Vector3(0, 0, transform.position.z)) > distanceThreshold) {
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
        } else {

            Vector3 goalDir = (new Vector3(0, 0, upSpeed) + new Vector3(0, 0, -downSpeed)) * maxSpeed;

            if (goalPosition.transform.position.z > transform.position.z) {
                downSpeed = 0f;
                upSpeed += Time.deltaTime * acceleration;
                upSpeed = Mathf.Clamp01(upSpeed);
                rb.velocity = goalDir;
            } else {
                upSpeed = 0f;
                downSpeed += Time.deltaTime * acceleration;
                downSpeed = Mathf.Clamp01(downSpeed);
                rb.velocity = goalDir;
            }
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




        if (gameActive) {
            if (hitWeb) {
                tfSpeed += Time.deltaTime * 0.01f;
                deltaPos = transform.right * tfSpeed * Time.deltaTime / 2;
                transform.position += deltaPos;
            } else {
                tfSpeed += Time.deltaTime * 0.01f;
                deltaPos = transform.right * tfSpeed * Time.deltaTime;
                transform.position += deltaPos;

            }
        }

        if (cameraMove) {
            Vector3 camPos = transform.position;
            camPos.x += 5f;
            camPos.y += 10f;
            camPos.z = 0;
            Camera.main.transform.position = camPos;
        }
    }

    public void ResetCharacter() {
        tfSpeed = tfSpeedOnStart;
        rb.velocity = Vector3.zero;
        transform.position = charStartPos;
        speedFactor = 0f;
        gameActive = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        hitWeb = false;
        cameraMove = true;
        canMove = true;
    }

}


