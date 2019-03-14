using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatterLaunch : MonoBehaviour
{
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            LaunchSwatAnim();
        }
    }

    void LaunchSwatAnim() {
        animator.Play("FlyswatterHit");
    }
}
