using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kosti_LeafClick : MonoBehaviour {

    Animator animator;
    public List<string> animationPool;
    float animTimer = 5f;
    public float minT;
    public float maxT;
    public bool leafsVisible;



    private void Start() {
        animator = GetComponent<Animator>();
        leafsVisible = true;
    }

    private void Update() {
        if (leafsVisible) {
            animTimer -= Time.deltaTime;
            if (animTimer < 0) {
                animator.Play(animationPool[Random.Range(0, animationPool.Count)]);
                animTimer = Random.Range(minT, maxT);
            }
        }

    }

    private void OnMouseDown() {
        animator.Play("ClickLeafs");
        leafsVisible = false;
        GetComponentInChildren<ParticleSystem>().Play();
    }
}
