﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuKaarlo_drag : MonoBehaviour {

    public Sprite dragSprite;
    public GameObject draggableObject;
    Animator animator;
    public string emptyAnimation;
    public string defaultAnimation;
    public GameObject jigsawHalo;
    public GameObject draggablesFolder;

    ScrollRect sLock;
    GameObject kaarloScrollRect;

    AnimatorTimer at;

    
    void Start() {
        kaarloScrollRect = GameObject.Find("Page5_Kaarlo");
        sLock = kaarloScrollRect.GetComponent<ScrollRect>();
        animator = GetComponent<Animator>();
    }

    public void ResetState() {
        if (at) {
            at.enabled = true;
        }
        this.enabled = true;
        animator.Play(defaultAnimation);
        sLock.vertical = enabled;
        jigsawHalo.SetActive(false);
    }

    public void ResetHalo() {
        this.enabled = true;
        sLock.vertical = enabled;
        jigsawHalo.SetActive(false);
    }

    private void OnMouseDown() {
        at = GetComponent<AnimatorTimer>();
        if (at) {
            at.enabled = false;
        }

        if (!enabled) {
            return;
        }
        jigsawHalo.SetActive(true);
        sLock.vertical = !enabled;
        animator.Play(emptyAnimation);
        var dragAnimal = Instantiate(draggableObject, transform.position, Quaternion.identity);
        dragAnimal.GetComponent<Transform>().parent = draggablesFolder.transform;
        dragAnimal.GetComponent<AnimalDrag_Kaarlo>().mkd = this;
        this.enabled = false;
    }

    
}
