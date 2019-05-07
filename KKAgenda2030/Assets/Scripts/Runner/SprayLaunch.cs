using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayLaunch : MonoBehaviour
{
    Animator animator;
    ParticleSystem ps;

    public AudioSource sprayAudio;
    public AudioClip raid;

    void Start()
    {
        animator = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            sprayAudio.PlayOneShot(raid);
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
