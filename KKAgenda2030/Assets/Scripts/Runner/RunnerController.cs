using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour {

    public float speed;
    public float speedFactor;
    public float maxSpeed;

    public LayerMask background;

    bool up;
    bool down;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    //void Update() {
    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, background)) {
    //        if (Input.GetKey(KeyCode.Mouse0)) {
    //            speedFactor += Time.deltaTime;
    //            speedFactor = Mathf.Clamp01(speedFactor);
    //        } else {
    //            speedFactor -= Time.deltaTime;
    //            speedFactor = Mathf.Clamp01(speedFactor);
    //        }
    //    }

    //    bool dir = rb.velocity.z > 0 ?
    //           up = true :
    //           up = false;

    //        if (hit.point.z > transform.position.z)
    //            rb.velocity = transform.forward * speedFactor;
    //        else if (hit.point.z < transform.position.z)
    //            rb.velocity = -transform.forward * speedFactor;

    //        //rb.velocity = transform.right * speed;

    //        Vector3 camPos = transform.position;
    //        camPos.y += 10f;
    //        Camera.main.transform.position = camPos;

    //        //sprintFactor = sprintT > sprintDuration ?
    //        //0f :
    //        //0.5f * (Mathf.Sin(sprintT * 2 * Mathf.PI / sprintDuration - 0.5f * Mathf.PI) + 1);

           
    //        //if (rb.velocity.z > 0)
    //        //    up = true;
    //        //else
    //        //    up = false;
    //        //else if (rb.velocity.z < 0)
        
    //}

    void Update() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, background)) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                speedFactor += Time.deltaTime;
                speedFactor = Mathf.Clamp01(speedFactor);
            } else {
                speedFactor -= Time.deltaTime;
                speedFactor = Mathf.Clamp01(speedFactor);
            }
        }

        bool dir = rb.velocity.z > 0 ?
               up = true :
               up = false;

        if (hit.point.z > transform.position.z)
            rb.velocity = transform.forward * speedFactor;
        else if (hit.point.z < transform.position.z)
            rb.velocity = -transform.forward * speedFactor;

        //rb.velocity = transform.right * speed;

        Vector3 camPos = transform.position;
        camPos.y += 10f;
        Camera.main.transform.position = camPos;

        //sprintFactor = sprintT > sprintDuration ?
        //0f :
        //0.5f * (Mathf.Sin(sprintT * 2 * Mathf.PI / sprintDuration - 0.5f * Mathf.PI) + 1);


        //if (rb.velocity.z > 0)
        //    up = true;
        //else
        //    up = false;
        //else if (rb.velocity.z < 0)

    }
}


