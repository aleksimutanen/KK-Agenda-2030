using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClickBehaviour : MonoBehaviour {

    MemoryGameManager mgm;
    public int thisPicIndx;
    public Animator animator;
    public GameObject halo;

    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
        animator = GetComponent<Animator>();
    }

    public void OnSelected() {
        animator.Play("AvatarGlow");
        halo.SetActive(true);
    }

    public void OnDeselected() {
        animator.Play("New State");
        halo.SetActive(false);

    }

    public void ClickAvatar() {
        mgm.SlotSelected(thisPicIndx);
    }
}
