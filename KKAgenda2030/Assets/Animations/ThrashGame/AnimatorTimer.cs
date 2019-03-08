using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTimer : MonoBehaviour {

    Animator animator;
    public List<string> animationPool;
    float animTimer = 5F;
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
