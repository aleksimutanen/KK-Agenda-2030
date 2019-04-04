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

        //for (int i = 0; i < playerAvatars.Count; i++) {
        //    var acb = FindObjectOfType<AvatarClickBehaviour>().currentlySelected = false;
        //}
        //var currentPicIndx = mgm.pictureIndx;
        //if (currentPicIndx != thisPicIndx) {
        //    mgm.pictureIndx = thisPicIndx;
        //    currentlySelected = true;
        //    // toggle some effect?
        //    print("image hit, " + "pictureIndx is now " + thisPicIndx);

        //}
    }
}
