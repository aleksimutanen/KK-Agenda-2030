using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTimer : MonoBehaviour {

    Animator animator;
    public List<string> animationPool;
    float animTimer;
    public float minT;
    public float maxT;
    public bool animatioPlaying = false;

    void Start() {
        animator = GetComponent<Animator>();
        animTimer = Random.Range(2, 5);
    }

    void Update() {
        animTimer -= Time.deltaTime;
        if(animTimer < 0 && !animatioPlaying) {
            animator.Play(animationPool[Random.Range(0, animationPool.Count)]);
            animTimer = Random.Range(minT, maxT);
        }
    }

    public void AnimationBoolTrue() {
        animatioPlaying = true;
    }

    public void AnimationBoolFalse() {
        animatioPlaying = false;
    }
}
