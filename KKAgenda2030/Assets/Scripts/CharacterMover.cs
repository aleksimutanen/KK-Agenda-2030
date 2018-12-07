﻿using System.Collections;
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
    float pushTimer = 0.3f;
    float moveInputStartTime;

    public LayerMask background;
    public LayerMask walls;
    public LayerMask collectables;

    public Vector3 playerYPos;
    public Vector3 rayOffset;
    public Vector3 newDir;
    public Vector3 startPos;
    Quaternion startRot;

    public bool canMove;
    public bool pushing;
    bool decelerating;
    bool accelerating;
    bool happyFaceActive;

    public List<Collider> thisWall = new List<Collider>();
    public GameObject /*unHappyHead*/u;
    public GameObject /*happyHead*/h;

    Rigidbody rb;
    PhoneVibrate pv;
    Animator am;
    SpriteRenderer sr;
    SpriteRenderer unHappyHead;
    SpriteRenderer happyHead;

    public SpriteRenderer[] faces;
    public List<Collider> items;

    void Start() {
        canMove = true;
        am = GetComponentInChildren<Animator>();

        sr = GetComponentInChildren<SpriteRenderer>();
        unHappyHead = u.GetComponent<SpriteRenderer>();
        happyHead = h.GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody>();
        pv = FindObjectOfType<PhoneVibrate>();
        startPos = transform.position;
        startRot = transform.rotation;
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

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

    void FixedUpdate() {
        //graphics
        if (transform.eulerAngles.y > -90f && transform.eulerAngles.y < 180f)
            FlipSpriteYPos();
        else FlipSpriteYNeg();

        var s = Physics.OverlapSphere(transform.position, 2f, collectables);
        items = new List<Collider>(s);
        for (int i = 0; i < items.Count; i++) {
            if (items[i].gameObject.name == "Food(Clone)")
                ChangeFace();
            else if (!happyFaceActive) DisableFace();
        }


        //movement
        if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            moveInputStartTime = Time.time;
        var sprintT = Time.time - moveInputStartTime;
        // wave 0..1
        sprintFactor = sprintT > sprintDuration ?
            0f :
            0.5f * (Mathf.Sin(sprintT * 2 * Mathf.PI / sprintDuration - 0.5f * Mathf.PI) + 1);
        sprintFactor *= (maxSprintSpeed - 1);
        sprintFactor += 1f;
        if ((Input.GetKey(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) && canMove) {
            RaycastHit hitGround;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitGround, Mathf.Infinity, background) && Vector3.Distance(transform.position, hitGround.point) > 0.75f &&
                !pushing && thisWall.Count == 0 && !accelerating) {
                speedFactor += Time.deltaTime * acceleration;
                speedFactor = Mathf.Clamp01(speedFactor);

                Vector3 targetDir = hitGround.point + playerYPos - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                float maxRD = turningSpeed * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);
            } else {
                speedFactor -= Time.deltaTime * deceleration;
                speedFactor = Mathf.Clamp01(speedFactor);
            }
        } else {
            speedFactor -= Time.deltaTime * deceleration;
            speedFactor = Mathf.Clamp01(speedFactor);
        }
        if (pv.nets.Count > 0) {
            rb.velocity = transform.forward * (speedFactor / 2) * sprintFactor * maxCharacterSpeed;
            am.Play("UnHappySharkSwim");
        } else if (!pushing) {
            rb.velocity = transform.forward * speedFactor * sprintFactor * maxCharacterSpeed;
        }
        if (speedFactor > 0 && pv.nets.Count == 0) am.Play("SharkSwim");
        else if (speedFactor == 0 && pv.nets.Count == 0) am.Play("SharkIdle");

        Debug.DrawLine(transform.position - transform.forward, transform.position + transform.forward * 100f, Color.red);
    }

    void OnTriggerStay(Collider other) {
        if (!thisWall.Contains(other) && thisWall.Count == 0 && other.gameObject.tag == "Wall")
            thisWall.Add(other);
        if (other.gameObject.tag == "Wall" && thisWall.Contains(other)) {
            if (!pushing) {
                RaycastHit hitWall;
                Physics.Raycast(transform.position - transform.forward, transform.forward, out hitWall, Mathf.Infinity, walls);
                print(hitWall.normal);
                if (hitWall.normal == Vector3.zero)
                    hitWall.normal = other.transform.forward;
                Vector3 reflectDir = Vector3.Reflect(transform.forward, hitWall.normal);
                newDir = reflectDir + hitWall.normal;
                newDir.y = 0f;
                pushFactor = speedFactor;
                pushing = true;
                decelerating = true;
            }

            pushTimer -= Time.deltaTime;

            if (decelerating && pushTimer < 0) {
                Vector3 targetDir = newDir + playerYPos;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                float maxRD = (turningSpeed / 1.5f) * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);

                pushFactor -= Time.deltaTime * (deceleration * 2);
                rb.velocity = transform.forward * pushFactor * sprintFactor * maxCharacterSpeed;

                if (pushFactor < 0) {
                    decelerating = false;
                    accelerating = true;
                }
            }

            if (accelerating) {
                Vector3 targetDir = newDir + playerYPos;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                float maxRD = (turningSpeed / 1.5f) * Time.deltaTime;
                rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, maxRD);

                pushFactor += Time.deltaTime * acceleration;
                pushFactor = Mathf.Clamp01(pushFactor);
                rb.velocity = /*newDir*/transform.forward * pushFactor * sprintFactor * maxCharacterSpeed;
            }
        }
    }

    void ChangeFace() {
        if (happyHead.enabled) return;
        happyHead.enabled = true;
        print("face changed");
    }

    void DisableFace() {
        if (!happyHead.enabled) return;
            happyHead.enabled = false;
        print("face disabled");
    }

    public IEnumerator AteTrash() {
        //unHappyHead.SetActive(true);
        unHappyHead.enabled = true;
        yield return new WaitForSeconds(1f);
        //unHappyHead.SetActive(false);
        unHappyHead.enabled = false;
    }

    public IEnumerator AteFood() {
        happyFaceActive = true;
        //happyHead.SetActive(true);
        happyHead.enabled = true;
        yield return new WaitForSeconds(1f);
        //happyHead.SetActive(false);
        happyHead.enabled = false;
        happyFaceActive = false;
    }

    public void FlipSpriteYNeg() {
        if (!sr.flipY) return;
        else {
            for (int i = 0; i < faces.Length; i++) faces[i].flipY = true;
            sr.flipY = false;
        }
    }

    public void FlipSpriteYPos() {
        if (sr.flipY) return;
        else {
            for (int i = 0; i < faces.Length; i++) faces[i].flipY = false;
            sr.flipY = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Wall") {
            speedFactor = pushFactor;
            accelerating = false;
            pushing = false;
            pushTimer = 0.3f;
            if (thisWall.Contains(other))
                thisWall.Remove(other);
        }
    }

    public void GrowScale() {
        transform.localScale += new Vector3(growScale, 0, growScale);
    }

    public void ResetCharacter() {
        var rb = GetComponent<Rigidbody>();
        canMove = true;
        transform.position = startPos;
        transform.rotation = startRot;
        rb.velocity = Vector3.zero;
    }
}
