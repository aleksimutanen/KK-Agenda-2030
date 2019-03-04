using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimals : MonoBehaviour {

    public Animator animator;
    public string showAnimation;
    public string happyAnimation;
    ToyRepairManager TRM;
    MenuGameManager MGM;


    private void Start() {
        TRM = FindObjectOfType<ToyRepairManager>();
        MGM = FindObjectOfType<MenuGameManager>();
    }

    private void OnMouseDown() {
        if (!TRM.haloVisible) {
            animator.Play(showAnimation);
        }
        if (MGM.animalsClickable) {
            animator.Play(happyAnimation);
        }
        else {
            animator.Play(happyAnimation);
        }
    }
}
