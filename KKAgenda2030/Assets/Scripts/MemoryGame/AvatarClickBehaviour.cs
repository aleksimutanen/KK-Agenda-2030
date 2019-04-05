using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClickBehaviour : MonoBehaviour {

    MemoryGameManager mgm;
    public int thisPicIndx;
    Animator animator;

    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
        animator = GetComponent<Animator>();
    }


    public void OnSelected() {
        animator.Play("AvatarGlow");
    }

    public void OnDeselected() {
        animator.Play("New State");
    }

    public void ClickAvatar() {
        mgm.SlotSelected(thisPicIndx);
    }
}
