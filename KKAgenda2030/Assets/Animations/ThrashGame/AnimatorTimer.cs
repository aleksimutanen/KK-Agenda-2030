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

    int random;

    void Awake() {
        animator = GetComponent<Animator>();
        animTimer = Random.Range(2, 5);
    }

    void Update() {
        animTimer -= Time.deltaTime;
        if(animTimer < 0 && !animatioPlaying) {
            random = Random.Range(0, animationPool.Count);
            animator.Play(animationPool[random]);
            animTimer = Random.Range(minT, maxT);
        }
    }

    public void AnimationBoolTrue() {
        animatioPlaying = true;
    }

    public void AnimationBoolFalse() {
        animatioPlaying = false;
    }

    public void OnEnable() {
        animator.Play(animationPool[random]);
    }


}
