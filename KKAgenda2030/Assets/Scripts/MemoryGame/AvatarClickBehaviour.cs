using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClickBehaviour : MonoBehaviour {

    MemoryGameManager mgm;
    public int thisPicIndx;
    public Animator animator;
    public GameObject halo;
    public GameObject grayImage;

    public AudioSource memorySound;
    public AudioClip changeTurn;

    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
        animator = GetComponent<Animator>();
    }

    public void OnSelected() {
        memorySound.PlayOneShot(changeTurn);
        animator.Play("AvatarGlow");
        halo.SetActive(true);
        grayImage.SetActive(false);
    }

    public void OnDeselected() {
        animator.Play("New State");
        halo.SetActive(false);
        grayImage.SetActive(true);

    }

    public void ClickAvatar() {
        mgm.SlotSelected(thisPicIndx);
    }
}
