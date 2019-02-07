using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLennu : MonoBehaviour {

    public Camera mainCamera;
    public Animator animator;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            Vector3 screenPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0f;
            animator.Play("Lennu_show");
        }
    }


}
