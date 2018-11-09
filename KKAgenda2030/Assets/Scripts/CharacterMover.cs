using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour {

    public float maxCharacterSpeed;
    public float maxSprintSpeed = 3f;
    public float sprintDuration = 2f;
    public float turningSpeed;
    float speedFactor;
    float sprintFactor;
    public float acceleration;
    public float deceleration;
    public float growScale;
    float pushFactor;
    float moveInputStartTime;

    public LayerMask background;
    public LayerMask walls;

    public Camera mainCam;

    public Vector3 camDist;
    public Vector3 playerYPos;
    public Vector3 newDir;

    public bool pushing;
    bool decelerating;
    bool accelerating;

    public List<Collider> thisWall = new List<Collider>();
    Collider wall;
    Rigidbody rb;

    void Start() {
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

    //y = 0.5*(sin(x*2*pi - 0.5*pi)+1)

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.touchCount > 0)
            moveInputStartTime = Time.time;
        var sprintT = Time.time - moveInputStartTime;
        // wave 0..1
        sprintFactor = sprintT > sprintDuration ?
            0f :
            0.5f * (Mathf.Sin(sprintT * 2 * Mathf.PI / sprintDuration - 0.5f * Mathf.PI) + 1);
        sprintFactor *= (maxSprintSpeed - 1);
        sprintFactor += 1f;
        if (Input.GetKey(KeyCode.Mouse0)) {
            RaycastHit hitGround;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitGround, Mathf.Infinity, background) && Vector3.Distance(transform.position, hitGround.point) > 0.75f &&
                !pushing && thisWall.Count == 0 && !accelerating) {
                speedFactor += Time.deltaTime * acceleration;
                speedFactor = Mathf.Clamp01(speedFactor);
                Vector3 targetDir = hitGround.point + playerYPos - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                float maxRD = turningSpeed * Time.deltaTime;
                //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, maxRD, 0.0f);
                //rb.rotation = Quaternion.LookRotation(newDir, Vector3.up);
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);
            } else {
                speedFactor -= Time.deltaTime * deceleration;
                speedFactor = Mathf.Clamp01(speedFactor);
            }
        } else {
            speedFactor -= Time.deltaTime * deceleration;
            speedFactor = Mathf.Clamp01(speedFactor);
        }
        if (!pushing) {
            rb.velocity = transform.forward * speedFactor * sprintFactor *  maxCharacterSpeed;
        }
        //if (mainCam.transform.position.x > -5 && mainCam.transform.position.x < 5 && mainCam.transform.position.z > 2 && mainCam.transform.position.z < 13.5)
        //var b = mainCam.transform.position;
        //b.x = Mathf.Clamp(b.x, -5f, 5f);
        //b.z = Mathf.Clamp(b.z, 2f, 13.5f);
        //mainCam.transform.position = b;
        //mainCam.transform.position = transform.position + camDist;
        Debug.DrawLine(transform.position, transform.position + transform.forward * 100f, Color.red);
    }

    void OnTriggerStay(Collider other) {
        if (!thisWall.Contains(other) && thisWall.Count == 0 && other.gameObject.tag == "Wall")
            thisWall.Add(other);
        if (other.gameObject.tag == "Wall" && thisWall.Contains(other)) {
            if (!pushing) {
                wall = thisWall[0];
                RaycastHit hitWall;
                Physics.Raycast(transform.position, transform.forward, out hitWall, Mathf.Infinity, walls);
                Vector3 reflectDir = Vector3.Reflect(transform.forward, hitWall.normal);
                newDir = reflectDir + hitWall.normal;
                pushFactor = 1;
                pushing = true;
                decelerating = true;
            }
            if (decelerating) {
                //Vector3 targetDir = wall.transform.forward + playerYPos - transform.position;

                //Vector3 targetDir = newDir + playerYPos;
                //Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                //float maxRD = turningSpeed * Time.deltaTime;
                //rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);

                //Vector3 targetDir = wall.transform.forward + playerYPos;
                //float maxRD = turningSpeed * 1.2f * Time.deltaTime;
                //Vector3 rotDir = Vector3.RotateTowards(transform.forward, targetDir, maxRD, 0.0f);
                //rb.rotation = Quaternion.LookRotation(rotDir);

                Vector3 targetDir = newDir + playerYPos;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                float maxRD = (turningSpeed / 2) * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);

                pushFactor -= Time.deltaTime * (deceleration * 2);
                rb.velocity = transform.forward * pushFactor * sprintFactor * maxCharacterSpeed;
                //rb.velocity = newDir * pushFactor * maxCharacterSpeed;
                if (pushFactor < 0) {
                    decelerating = false;
                    accelerating = true;
                }
            }
            if (accelerating) {
                //Vector3 targetDir = wall.transform.forward + playerYPos;
                //float maxRD = turningSpeed * 1.2f * Time.deltaTime;
                //Vector3 rotDir = Vector3.RotateTowards(transform.forward, targetDir, maxRD, 0.0f);
                //rb.rotation = Quaternion.LookRotation(rotDir);

                Vector3 targetDir = newDir + playerYPos;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                float maxRD = (turningSpeed / 2) * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);

                pushFactor += Time.deltaTime * acceleration;
                pushFactor = Mathf.Clamp01(pushFactor);
                rb.velocity = newDir * pushFactor * sprintFactor * maxCharacterSpeed;
                //rb.velocity = wall.transform.forward.normalized * pushFactor * maxCharacterSpeed;
            }
        }
    }

    //private void OnTriggerStay(Collider other) {
    //    if (!thisWall.Contains(other) && thisWall.Count == 0 && other.gameObject.tag == "Wall")
    //        thisWall.Add(other);
    //    if (other.gameObject.tag == "Wall" && thisWall.Contains(other)) {
    //        rb.velocity -= transform.forward;
    //    }
    //}

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Wall") {
            speedFactor = pushFactor;
            accelerating = false;
            pushing = false;
            if (thisWall.Contains(other))
                thisWall.Remove(other);
        }
    }

    public void GrowScale() {
        transform.localScale += new Vector3(growScale, 0, growScale);
    }
}
