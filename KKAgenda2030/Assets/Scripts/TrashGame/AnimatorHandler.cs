using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator eyeAnimator;
    public string blinkAnimation;
    float blinkTimer;
    public float maxT;
    public float minT;

    void Start() {
        blinkTimer = Random.Range(minT, maxT);
    }

    void Update() {
        blinkTimer -= Time.deltaTime;
        if (blinkTimer < 0) {
            eyeAnimator.Play(blinkAnimation);
            blinkTimer = Random.Range(minT, maxT);
        }
    }
}
