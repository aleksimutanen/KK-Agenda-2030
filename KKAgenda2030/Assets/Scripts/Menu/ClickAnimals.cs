using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimals : MonoBehaviour {

    public Animator animator;
    public string showAnimation;
    public string happyAnimation;
    ToyRepairManager TRM;

    public AudioSource AnimalSound;
    public AudioClip AnimalNoise;


    private void Start() {
        TRM = FindObjectOfType<ToyRepairManager>();
    }

    private void OnMouseDown() {
        if (!TRM.haloVisible) {
            animator.Play(showAnimation);
        }
        else {
            animator.Play(happyAnimation);
        }
    }
}
