using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatterLaunch : MonoBehaviour
{
    Animator animator;
    ParticleSystem ps;

    public AudioSource swatAudio;
    public AudioClip swat;

    private void Awake() {
        animator = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            swatAudio.PlayOneShot(swat);
            LaunchSwatAnim();
        }
    }

    void LaunchSwatAnim() {
        animator.Play("FlyswatterHit");
    }

    public void ParticleEmit() {
        ps.Play();
    }
}
