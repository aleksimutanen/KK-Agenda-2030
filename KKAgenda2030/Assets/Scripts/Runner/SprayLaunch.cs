using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayLaunch : MonoBehaviour
{
    Animator animator;
    ParticleSystem ps;

    void Start()
    {
        animator = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            LaunchSprayAnim();
        }
    }

    void LaunchSprayAnim() {
        animator.Play("SpraycanSpray");
    }

    public void StartParticleEmit() {
        ps.Play();
    }
}
