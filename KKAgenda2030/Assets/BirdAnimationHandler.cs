using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimationHandler : MonoBehaviour {

    Animator animator;
    public List<string> animationPool;
    float animTimer;
    public float minT;
    public float maxT;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        animTimer -= Time.deltaTime;
        if(animTimer < 0) {
            animator.Play(animationPool[Random.Range(0, animationPool.Count)]);
            animTimer = Random.Range(minT, maxT);
        }
    }
}
