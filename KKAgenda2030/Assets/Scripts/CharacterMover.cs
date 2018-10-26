using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

    public float xRot;
    public float characterSpeed;
    public float maxCharacterSpeed;
    public float turningSpeed;
    public float slowerSpeed;
    public float speedFactor;
    public float acceleration;
    public float deceleration;

    public LayerMask background;
    public LayerMask walls;

    public Camera mainCam;

    public Vector3[] directions;
    public Vector3 camDist;
    public Vector3 playerYPos;
    public Vector3 rayOffset;

    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}

    //void Update() {
    //    transform.position += transform.rotation * Vector3.forward * characterSpeed;
    //    mainCam.transform.position = transform.position + camDist;
    //    if (Input.GetKey(KeyCode.Mouse0)) {
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        //var rot = transform.rotation.normalized;
    //        //var rot = transform.rotation;
    //        var rot = transform.eulerAngles;
    //        //var rot = transform.localRotation;
    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, background)) {
    //            if (hit.point.z > transform.position.z) {
    //                //if (transform.eulerAngles.y < 90/* && transform.localEulerAngles.y >= 0*/) {
    //                print(transform.eulerAngles.y);
    //                print("turn right");
    //                rot.y += turningSpeed * Time.deltaTime;
    //                rot.x = 0; rot.z = 0;
    //                //}
    //            } else if (hit.point.z < transform.position.z) {
    //                //if (transform.eulerAngles.y > -90/* && transform.localEulerAngles.y <= 0*/) {
    //                print(transform.eulerAngles.y);
    //                print("turn left");
    //                rot.y -= turningSpeed * Time.deltaTime;
    //                rot.x = 0; rot.z = 0;
    //                //}
    //            }
    //            //rot.y = Mathf.Clamp(rot.y, 0, 270);
    //            transform.eulerAngles = rot;
    //            mainCam.transform.position = transform.position + camDist;
    //        }
    //    }
    //}
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            RaycastHit hitGround;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitGround, Mathf.Infinity, background) && Vector3.Distance(transform.position, hitGround.point) > 0.5f) {
                speedFactor += Time.deltaTime * acceleration;
                speedFactor = Mathf.Clamp01(speedFactor);
                Vector3 targetDir = hitGround.point + playerYPos - transform.position;
                float maxRD = turningSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, maxRD, 0.0f);
                rb.rotation = Quaternion.LookRotation(newDir);
            } else {
                speedFactor -= Time.deltaTime * deceleration;
                speedFactor = Mathf.Clamp01(speedFactor);
            }
        } else {
            speedFactor -= Time.deltaTime * deceleration;
            speedFactor = Mathf.Clamp01(speedFactor);
        }
        rb.velocity = transform.forward * speedFactor * maxCharacterSpeed;
        mainCam.transform.position = transform.position + camDist;
    }
}
